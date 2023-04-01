using System;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.UI;
using Code.Services;
using Code.UI.Windows;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Management
{
    public class Shop : IShopService
    {
        private IResourceService _resourceRepository;
        private GardenDataHolder _gardenDataHolder;
        private CropsDataHolder _cropsDataHolder;
        private CropsShopData _cropsShopData;
        private GardenData _gardenData;

        public event Action<GardenData> SoldGarden;
        public event Action SoldGridCells;
        
        public void Init(IResourceService resourceRepository,GardenDataHolder gardenDataHolder, 
            CropsDataHolder cropsDataHolder)
        {
            _resourceRepository = resourceRepository;
            _gardenDataHolder = gardenDataHolder;
            _cropsDataHolder = cropsDataHolder;
        }

        public void BuyGarden(ProductType type)
        {
            foreach (CropsShopData cropsData in _cropsDataHolder.CropsDataList.
                         Where(cropsData => cropsData.ProductType == type))
                _cropsShopData = cropsData;

            foreach (GardenData gardenData in _gardenDataHolder.List.
                         Where(gardenData => gardenData.ProductType == type))
                _gardenData = gardenData;

            if (_resourceRepository.CanAfford(_cropsShopData.PriceData)) 
                SoldGarden?.Invoke(_gardenData);
        }

        private void BuyCells(int gold)
        {
            
        }
    }
}