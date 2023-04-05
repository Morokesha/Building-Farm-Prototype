using System.Collections.Generic;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    {
        public ResourceHolder ResourceHolder => _resourceHolder;
        public ShopItemDataHolder ShopItemDataHolder => _shopItemDataHolder;
        
        private GardenDataHolder _gardenDataHolder;
        private ResourceHolder _resourceHolder;
        private ShopItemDataHolder _shopItemDataHolder;
        private ShopItemData _shopItemData;
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
        }
    }
}