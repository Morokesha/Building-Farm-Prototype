using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Services.ResourceServices;

namespace Code.Services.ShopServices
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