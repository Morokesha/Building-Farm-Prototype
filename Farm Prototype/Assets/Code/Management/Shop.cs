using System;
using Code.Data.GardenBedData;
using Code.UI;
using Code.Services;
using UnityEngine;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;
        private GardenTypeHolder _gardenTypeHolder;
        
        private ShopUI _shopUI;

        private GardenData _gardenData;
        
        public event Action<SeedType> SoldGardenBed;
        public event Action SoldCells;
        
        public void Init(IResourceService resourceRepository,GardenTypeHolder gardenTypeHolder,ShopUI shopUI)
        {
            _resourceRepository = resourceRepository;
            _gardenTypeHolder = gardenTypeHolder;

            _shopUI = shopUI;
            _shopUI.BuyWheat += ShopUIOnBuyWheat;
            
            Debug.Log(_gardenTypeHolder);
        }

        private void ShopUIOnBuyWheat(SeedType type)
        {
            BuyGardenBed(type);
        }

        private void BuyCells(int coins)
        {
            
        }
        
        private void BuyGardenBed(SeedType type)
        {
            foreach (var gardenBedData in _gardenTypeHolder.List)
            {
                if (gardenBedData.SeedType == type)
                {
                    _gardenData = gardenBedData;
                }
            }
            
            if (_resourceRepository.CanAfford(_gardenData.GardenCostArray)) 
                SoldGardenBed?.Invoke(type);
        }

        public void Clear()
        {
            _shopUI.BuyWheat -= ShopUIOnBuyWheat;
        }
    }
}