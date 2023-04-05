using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.UI;
using Code.UI.Windows;

namespace Code.Services
{
    public interface IShopService
    {
        public event Action ProductPurchased;
        event Action<GardenData> SoldGarden;
        event Action SoldGridCells;
        void Init(IResourceService resourceRepository);
        void BuyGarden(ShopItemData shopItemData,GardenData gardenData);
    }
}