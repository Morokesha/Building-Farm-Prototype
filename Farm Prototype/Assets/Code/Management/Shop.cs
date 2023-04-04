using System;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.UI;
using Code.Services;
using Code.UI.Windows;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;
        private GardenDataHolder _gardenDataHolder;
        private ShopItemDataHolder _shopItemDataHolder;
        private ShopItemData _shopItemData;
        private IStaticDataService _staticDataService;

        public event Action<GardenData> SoldGarden;
        public event Action SoldGridCells;
        
        public void Init(IResourceService resourceRepository, IStaticDataService staticDataService)
        {
            _resourceRepository = resourceRepository;
            _staticDataService = staticDataService;
        }

        public void BuyGarden(GardenData gardenData)
        {
            if (_resourceRepository.CanAfford(_shopItemData.PriceData)) 
                SoldGarden?.Invoke(gardenData);
        }

        private void BuyCells(int gold)
        {
            
        }
    }
}