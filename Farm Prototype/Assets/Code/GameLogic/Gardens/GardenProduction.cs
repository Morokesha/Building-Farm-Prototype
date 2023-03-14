using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.Management;
using Code.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public class GardenProduction
    {
        private readonly IResourceService _resourceRepository;

        private int _currentChanceDrop;
        private readonly int _percentDropSeed = 20;
        private readonly int _startProductScale = 0;
        private readonly int _finishProductScale = 1;

        private bool _growingCompleted;
        public GardenProduction(IResourceService resourceRepository) => 
            _resourceRepository = resourceRepository;

        public void Growing(GardenData gardenData,RowsProducts rowsProducts)
        {
            Vector3 rowProductsLocalScale = rowsProducts.transform.localScale;
            float localScaleY = Mathf.Lerp
                (_startProductScale, _finishProductScale, gardenData.GeneratorData.TimeGrowingCrops);
            rowProductsLocalScale.y = localScaleY;
            rowsProducts.transform.localScale = rowProductsLocalScale;

            if (localScaleY >= _finishProductScale)
            {
                _growingCompleted = true;
            }
        }
        
        public void Harvesting(ResourceGeneratorData resourceGeneratorData)
        {
            foreach (var resourceAmountData in resourceGeneratorData.ResourceGeneratorAmounts)
            {
                if (resourceAmountData.ResourceData.Type == ResourceType.Coin)
                    AddRandomResources(resourceAmountData);
                
                else if(resourceAmountData.ResourceData.Type == ResourceType.Seed)
                {
                    _currentChanceDrop = Random.Range(0, 100);

                    if (_currentChanceDrop >= _percentDropSeed) 
                        AddRandomResources(resourceAmountData);
                }
            }
        }

        private void AddRandomResources(ResourceGeneratorAmount resourceAmountData)
        {
            int resource = Random.Range(resourceAmountData.MinAmount,
                resourceAmountData.MaxAmount);
            _resourceRepository.AddResource(resourceAmountData.ResourceData.Type, resource);
        }

        public bool GetGrowingCompleted() => _growingCompleted;
    }
}