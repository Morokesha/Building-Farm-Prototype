using System;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.UI;
using Code.Services;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;

        public event Action ProductPurchased;
        public event Action<GardenData> SoldGarden;
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

        private void BuyCells(int gold)
        {
            
        }
    }
}