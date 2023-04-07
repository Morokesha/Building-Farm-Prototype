using System.Collections.Generic;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Services.AssetServices;
using UnityEngine;

namespace Code.Services.StaticDataServices
{
    public class StaticDataService : IStaticDataService
    {
        public ResourceHolder ResourceHolder => _resourceHolder;
        public ShopItemDataHolder ShopItemDataHolder => _shopItemDataHolder;
        public List<ShopItemData> ShopItemsUpgradeData => _shopItemsUpgradeData;
        
        private GardenDataHolder _gardenDataHolder;
        private ResourceHolder _resourceHolder;
        private ShopItemDataHolder _shopItemDataHolder;
        private ShopItemData _shopItemData;
        private List<ShopItemData> _shopItemsUpgradeData;
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
            LoadItemUpgradeData();
        }

        private void LoadItemUpgradeData()
        {
            _shopItemsUpgradeData = new List<ShopItemData>();
            foreach (var item in _shopItemDataHolder.ShopItemDataList.
                         Where(item => item.ShopItemType == ShopItemType.Upgrade))
                _shopItemsUpgradeData.Add(item);
        }
    }
}