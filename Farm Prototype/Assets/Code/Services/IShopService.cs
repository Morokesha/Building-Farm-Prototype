using System;
using Code.Data.GardenBedData;

namespace Code.Services
{
    public interface IShopService
    {
        event Action<SeedType> SoldGardenBed;
        event Action SoldCells;
        void Clear();
    }
}