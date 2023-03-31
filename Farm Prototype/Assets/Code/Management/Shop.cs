using System;
using Code.Data.GardenData;
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
        private GardenTypeHolder _gardenTypeHolder;
        private GardenData _gardenData;
        
        public event Action<GardenData> SoldGarden;
        public event Action SoldGridCells;
        
        public void Init(IResourceService resourceRepository,GardenTypeHolder gardenTypeHolder)
        {
            _resourceRepository = resourceRepository;
            _gardenTypeHolder = gardenTypeHolder;
        }

        public void BuyGarden(ProductType type)
        {
            foreach (var gardenBedData in _gardenTypeHolder.List)
            {
                if (gardenBedData.productType == type) 
                    _gardenData = gardenBedData;
            }
            
            if (_resourceRepository.CanAfford(_gardenData.CropsShopData.PriceItem)) 
                SoldGarden?.Invoke(_gardenData);
        }

        private void BuyCells(int coins)
        {
            
        }
    }
}