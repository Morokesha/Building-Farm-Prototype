using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ResourceServices;
using Code.UI.Windows.Shop.WindowElements;

namespace Code.Services.ShopServices
{
    public interface IShopService
    {
        public event Action ProductPurchased;
        event Action<GardenData,ShopItemData> SoldGarden;
        public event Action<UpgradeItemData,ContentItem> SoldUpgrade;
        void Init(IResourceService resourceRepository);
        void BuyGarden(ShopItemData shopItemData,GardenData gardenData);
        void BuyUpgrade(UpgradeItemData upgradeData,ContentItem contentItem);
    }
}