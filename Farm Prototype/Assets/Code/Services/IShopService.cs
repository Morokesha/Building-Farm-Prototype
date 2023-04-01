using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.ShopTab;

namespace Code.Services
{
    public interface IShopService
    {
        event Action<GardenData> SoldGarden;
        event Action SoldGridCells;
        void Init(IResourceService resourceRepository, GardenDataHolder gardenDataHolder,
            CropsDataHolder cropsDataHolder);
        void BuyGarden(ProductType productType);
    }
}