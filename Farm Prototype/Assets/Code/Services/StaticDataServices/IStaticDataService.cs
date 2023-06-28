using System.Collections.Generic;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;

namespace Code.Services.StaticDataServices
{
    public interface IStaticDataService
    { 
        public ResourceHolder ResourceHolder { get; }
        public GardenData GetGardenData(ProductType type);
        public List<ShopItemData> LoadShopItemDataForType(ShopItemType type);
        public ShopItemData GetShopItemDataForUpgrade(UpgradeType type);
        public UpgradeItemData GetUpgradeData(UpgradeType upgradeType, UpgradeStage upgradeStage);
    }
}