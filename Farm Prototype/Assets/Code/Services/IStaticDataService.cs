using System.Collections.Generic;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;

namespace Code.Services
{
    public interface IStaticDataService
    { 
        public ResourceHolder ResourceHolder { get; }
        public ShopItemDataHolder ShopItemDataHolder { get; }
        public GardenData GetGardenData(ProductType type);
    }
}