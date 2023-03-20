﻿using System;
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
        public event Action<float> GrowingChanged;

        private readonly IResourceService _resourceRepository;
        private readonly GardenData _gardenData;
        private RowProducts _rowProducts;

        private int _currentChanceDrop;

        private readonly int _percentDropSeed = 20;
        private readonly int _startProductScale = 0;
        private readonly int _finishProductScale = 1;

        private float _growing;

        private ResourceType _harvestingResourceType;

        public GardenProduction(IResourceService resourceRepository,GardenData gardenData)
        {
            _resourceRepository = resourceRepository;
            _gardenData = gardenData;
        }

        public void SetRowProducts(RowProducts rowProducts) => 
            _rowProducts = rowProducts;

        public void Growing()
        {
            Vector3 rowProductsLocalScale = _rowProducts.transform.localScale;

            rowProductsLocalScale.y = _startProductScale;
            
            _growing = Mathf.Lerp
                (_startProductScale, _finishProductScale, _gardenData.GeneratorData.TimeGrowingCrops);
            
            GrowingChanged?.Invoke(_growing);
            
            rowProductsLocalScale.y = _growing;
            _rowProducts.transform.localScale = rowProductsLocalScale;

            FinishGrowing();
        }

        private void FinishGrowing()
        {
            if (_growing >= _finishProductScale)
            {
                _currentChanceDrop = Random.Range(0, 100);

                if (_currentChanceDrop <= _percentDropSeed)
                {
                    _harvestingResourceType = ResourceType.Seed;
                    ActivatedHarvesting?.Invoke(_harvestingResourceType);
                    Debug.Log("HarvestSeed");
                }

                _harvestingResourceType = ResourceType.Gold;
                ActivatedHarvesting?.Invoke(_harvestingResourceType);
                Debug.Log("HarvestGold");
            }
        }

        public void Harvesting(ResourceType type)
        {
            _resourceRepository.AddResource(type,
                type == ResourceType.Gold ? _gardenData.GeneratorData.CoinAmout : 
                    _gardenData.GeneratorData.SeedAmount);
        }
    }
}