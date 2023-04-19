﻿using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.ShopData.UpgradeData;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;

        public event Action ProductPurchased;
        public event Action<GardenData> SoldGarden;
        public event Action<UpgradeItemData> SoldUpgrade;
        public event Action SoldGridCells;
        
        public void Init(IResourceService resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public void BuyGarden(ShopItemData shopItemData,GardenData gardenData)
        {
            if (_resourceRepository.CanAfford(shopItemData.PriceData))
            {
                _resourceRepository.SpendResources(shopItemData.PriceData);
                SoldGarden?.Invoke(gardenData);
                ProductPurchased?.Invoke();
            }
        }

        public void BuyUpgrade(ShopItemData shopItemData, UpgradeItemData upgradeData)
        {
            if (_resourceRepository.CanAfford(shopItemData.PriceData))
            {
                _resourceRepository.SpendResources(shopItemData.PriceData);
                SoldUpgrade?.Invoke(upgradeData);
                ProductPurchased?.Invoke();
            }
        }
    }
}