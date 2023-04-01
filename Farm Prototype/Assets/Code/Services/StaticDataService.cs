using System.Collections.Generic;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    { 
        public GardenDataHolder GardenDataHolder => _gardenDataHolder; 
        public ResourceHolder ResourceHolder => _resourceHolder;
        public CropsDataHolder CropsDataHolder => _cropsDataHolder;
    
        private List<GardenData> _gardeDataHolder;
        private List<CropsShopData> _cropsShopDataList;
        
        private GardenDataHolder _gardenDataHolder;
        private ResourceHolder _resourceHolder;
        private CropsDataHolder _cropsDataHolder;

        private void LoadData()
        {
            _gardenDataHolder = Resources.Load<GardenDataHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
        }
    }
}