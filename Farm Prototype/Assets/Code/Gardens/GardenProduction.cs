using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Management;
using UnityEngine;

namespace Code.Gardens
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

        public void ProductionIsDone(ResourceGeneratorData resourceGeneratorData)
        {
            foreach (var resourceAmount in resourceGeneratorData.ResourceAmounts)
            {
                if (resourceAmount.ResourceData.Type == ResourceType.Coin)
                {
                    int coin = Random.Range(resourceAmount.MinAmount, 
                        resourceAmount.MinAmount);
                    _resourceRepository.AddResource(resourceGeneratorData.ResourceAmounts,coin);
                }
                else
                {
                    int seed = Random.Range(resourceAmount.MinAmount, 
                        resourceAmount.MinAmount);
                    _resourceRepository.AddResource(resourceGeneratorData.ResourceAmounts,seed);
                }
            }
        }
    }
}