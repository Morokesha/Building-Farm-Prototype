using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Data.ShopData;

namespace Code.Services
{
    public interface IStaticDataService
    {
        public GardenDataHolder GardenDataHolder { get; }
        public ResourceHolder ResourceHolder { get; }
        public CropsDataHolder CropsDataHolder { get; }
    }
}