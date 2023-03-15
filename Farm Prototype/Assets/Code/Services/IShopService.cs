using System;
using Code.Data.GardenBedData;
using Code.UI;

namespace Code.Services
{
    public interface IShopService
    {
        event Action<SeedType> SoldGardenBed;
        event Action SoldCells;
        void Init(IResourceService resourceRepository, GardenTypeHolder gardenTypeHolder, ShopUI shopUI);
        void Clear();
    }
}