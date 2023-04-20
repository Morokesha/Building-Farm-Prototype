using System.Collections.Generic;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Data.ShopData.UpgradeData;
using Code.Services.AssetServices;
using UnityEngine;

namespace Code.Services.StaticDataServices
{
    public class StaticDataService : IStaticDataService
    {
        public ResourceHolder ResourceHolder => _resourceHolder;

        private GardenDataHolder _gardenDataHolder;
        private ResourceHolder _resourceHolder;
        private ShopItemDataHolder _shopItemDataHolder;
        private List<ShopItemData> _shopItemDataList;
        private List<UpgradeItemData> _upgradeItemDataList;
        private GardenData _gardenData;

        public StaticDataService() => 
            LoadData();

        public GardenData GetGardenData(ProductType type)
        {
            foreach (var data in _gardenDataHolder.List.Where(data => type == data.ProductType)) 
                _gardenData = data;

            return _gardenData;
        }

        private void LoadData()
        {
            _gardenDataHolder = Resources.Load<GardenDataHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
            _shopItemDataHolder = Resources.Load<ShopItemDataHolder>(AssetPath.ShopItemDataHolderPath);
            _shopItemDataList = new List<ShopItemData>();

            LoadShopItemData();
            LoadUpgradeData();
        }

        private void LoadShopItemData()
        {
            foreach (var item in _shopItemDataHolder.ShopItemDataList) 
                _shopItemDataList.Add(item);
        }

        private void LoadUpgradeData() => 
            _upgradeItemDataList = Resources.LoadAll<UpgradeItemData>(AssetPath.UpgradeDataPath).ToList();

        public List<ShopItemData> LoadShopItemDataForType(ShopItemType type) => 
            _shopItemDataList.Where(item => item.ShopItemType == type).ToList();

        public UpgradeItemData GetUpgradeData(UpgradeType upgradeType)
        {
            return _upgradeItemDataList.
                FirstOrDefault(upgradeItemData => upgradeItemData.UpgradeType == upgradeType);
        }
    }
}