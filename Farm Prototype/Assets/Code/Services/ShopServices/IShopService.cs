using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ResourceServices;

namespace Code.Services.ShopServices
{
    public interface IShopService
    {
        public event Action ProductPurchased;
        event Action<GardenData,ShopItemData> SoldGarden;
        public event Action<UpgradeItemData> SoldUpgrade;
        void Init(IResourceService resourceRepository);
        void BuyGarden(ShopItemData shopItemData,GardenData gardenData);
        void BuyUpgrade(ShopItemData shopItemData, UpgradeItemData upgradeData);
    }
}