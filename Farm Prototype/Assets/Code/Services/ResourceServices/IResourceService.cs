using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Services.ProgressServices;

namespace Code.Services.ResourceServices
{
    public interface IResourceService
    {
        void Init(IProgressDataService progressDataService,ResourceHolder resourceHolder);
        void AddGold(DropData dropData);
        void AddSeed(DropData dropData);
        void SpendResources(PriceData priceData);
        bool CanAfford(PriceData priceData);
    }
}