using System;
using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.GameLogic.Gardens
{
    public class GardenProduction
    {
        public event Action<ResourceType> ActivatedHarvesting;
        public event Action GrowingComleted; 

        private readonly IResourceService _resourceRepository;
        private readonly GardenData _gardenData;

        private int _currentChanceDrop;
        private readonly int _percentDropSeed = 20;
        private readonly int _startProductScale = 0;
        private readonly int _finishProductScale = 1;

        private float _growing;

        public GardenProduction(IResourceService resourceRepository,GardenData gardenData)
        {
            _resourceRepository = resourceRepository;
            _gardenData = gardenData;
        }

        public void Growing(RowProducts rowProducts)
        {
            Vector3 rowProductsLocalScale = rowProducts.transform.localScale;
            _growing = Mathf.Lerp
                (_startProductScale, _finishProductScale, _gardenData.GeneratorData.TimeGrowingCrops);
            rowProductsLocalScale.y = _growing;
            rowProducts.transform.localScale = rowProductsLocalScale;

            FinishGrowing();
        }

        private void FinishGrowing()
        {
            if (_growing >= _finishProductScale)
            {
                _currentChanceDrop = Random.Range(0, 100);

                if (_currentChanceDrop >= _percentDropSeed)
                    ActivatedHarvesting?.Invoke(ResourceType.Coin);

                ActivatedHarvesting?.Invoke(ResourceType.Seed);
                
                GrowingComleted?.Invoke();
            }
        }

        public void Harvesting(ResourceType type)
        {
            _resourceRepository.AddResource(type,
                type == ResourceType.Coin ? _gardenData.GeneratorData.CoinAmout : 
                    _gardenData.GeneratorData.SeedAmount);
        }
    }
}