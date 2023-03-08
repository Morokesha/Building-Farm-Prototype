using Code.Data.ResourceData;
using Code.Management;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public class GardenProduction
    {
        private readonly ResourceRepository _resourceRepository;

        private float _timeProduction;
        private float _currentScale;
        private float _scaleProduction;
        
        public GardenProduction(ResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        
        public void ScalerProduction(float deltaTime)
        {
            _currentScale += deltaTime;
            _scaleProduction = _currentScale / _timeProduction;

            if (_scaleProduction >= _timeProduction)
            {
                _currentScale = 0;
            }
        }

        public void ProductsGrown(ResourceGeneratorData resourceGeneratorData)
        {
            foreach (var resourceAmountData in resourceGeneratorData.ResourceGeneratorAmounts)
            {
                if (resourceAmountData.ResourceData.Type == ResourceType.Coin)
                {
                    int coinAmount = Random.Range(resourceAmountData.MinAmount, 
                        resourceAmountData.MaxAmount);
                    _resourceRepository.AddResource(resourceAmountData.ResourceData.Type,coinAmount);
                }
                else if(resourceAmountData.ResourceData.Type == ResourceType.Seed)
                {
                    int seedAmount = Random.Range(resourceAmountData.MinAmount, 
                        resourceAmountData.MaxAmount);
                    _resourceRepository.AddResource(resourceAmountData.ResourceData.Type,seedAmount);
                }
            }
        }
    }
}