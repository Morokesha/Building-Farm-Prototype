using System;
using Code.Data.GardenBedData;
using Code.UI;

namespace Code.Services
{
    public interface IShopService
    {
        event Action<GardenData> SoldGarden;
        event Action SoldGridCells;
        void Init(IResourceService resourceRepository, GardenTypeHolder gardenTypeHolder, ShopUI shopUI);
        void Clear();
    }
}