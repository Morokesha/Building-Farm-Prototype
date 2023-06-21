using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;

        public event Action ProductPurchased;
        public event Action<GardenData,ShopItemData> SoldGarden;
        public event Action<UpgradeItemData> SoldUpgrade;

        public void Init(IResourceService resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public void BuyGarden(ShopItemData shopItemData,GardenData gardenData)
        {
            if (_resourceRepository.CanAfford(shopItemData.PriceData))
            {
                SoldGarden?.Invoke(gardenData,shopItemData);
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