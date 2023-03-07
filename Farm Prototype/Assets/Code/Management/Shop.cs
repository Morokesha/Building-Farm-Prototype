﻿using System;
using Code.Data.GardenBedData;
using Code.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Management
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] 
        private ShopUI _shopUI;
        [FormerlySerializedAs("_resourceManager")] [SerializeField]
        private ResourceRepository resourceRepository;
        [SerializeField] 
        private GardenTypeHolder gardenTypeHolder;

        private GardenData _gardenData;
        
        public event Action<SeedType> SoldGardenBed;
        public event Action SoldCells;
        
        private void Start()
        {
            _shopUI.BuyWheat += ShopUIOnBuyWheat;
        }

        private void ShopUIOnBuyWheat(SeedType type)
        {
            BuyGardenBed(type);
        }

        private void BuyCells(int money)
        {
            
        }
        
        private void BuyGardenBed(SeedType type)
        {
            foreach (var gardenBedData in gardenTypeHolder.List)
            {
                if (gardenBedData.SeedType == type)
                {
                    _gardenData = gardenBedData;
                }
            }
            
            if (resourceRepository.CanAfford(_gardenData.GardenCostArray))
            {
                print("sold");
                SoldGardenBed?.Invoke(type);
            }
        }

        private void OnDisable()
        {
            _shopUI.BuyWheat -= ShopUIOnBuyWheat;
        }
    }
}