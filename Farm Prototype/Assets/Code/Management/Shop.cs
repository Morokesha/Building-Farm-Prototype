using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.UI.Windows.Shop.WindowElements;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;

        public event Action ProductPurchased;
        public event Action<GardenData,ShopItemData> SoldGarden;
        public event Action<UpgradeItemData,ContentItem> SoldUpgrade;

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

        public void BuyUpgrade(UpgradeItemData upgradeData,ContentItem contentItem)
        {
            if (_resourceRepository.CanAfford(upgradeData.PriceData))
            {
                _resourceRepository.SpendResources(upgradeData.PriceData);
                SoldUpgrade?.Invoke(upgradeData,contentItem);
                ProductPurchased?.Invoke();
            }
        }
    }
}