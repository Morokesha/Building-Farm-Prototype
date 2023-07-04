using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.UI.Windows.HUDWindow;
using Code.UI.Windows.Shop.WindowElements;

namespace Code.Management
{
    public class Shop : IShopService
    {
        public event Action ProductPurchased;
        public event Action<GardenData,ShopItemData> SoldGarden;
        public event Action<UpgradeItemData,ContentItem> SoldUpgrade;
        
        private IResourceService _resourceRepository;
        private HUD _hud;


        public void Init(IResourceService resourceRepository,HUD hud)
        {
            _resourceRepository = resourceRepository;
            _hud = hud;
        }

        public void BuyGarden(ShopItemData shopItemData,GardenData gardenData)
        {
            if (_resourceRepository.CanAfford(shopItemData.PriceData))
            {
                _hud.ActiveShopBtn(false);
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