using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;

namespace Code.Management
{
    public class ResourceRepository : IResourceService
    {
        private IProgressDataService _progressDataService;
        private ResourceHolder _resourceHolder;

        private int _goldAmount;
        private int _seedAmount;

        public void Init(IProgressDataService progressDataService, ResourceHolder resourceHolder)
        {
            _progressDataService = progressDataService;
            _resourceHolder = resourceHolder;

            StartingResources();
        }

        public void AddGold(DropData dropData)
        {
            _goldAmount += dropData.GoldAmount;
            _progressDataService.GoldenChanged(_goldAmount);
        }

        public void AddSeed(DropData dropData)
        {
            _seedAmount += dropData.SeedAmount;
            _progressDataService.SeedResourceChanged(_seedAmount);
        }

        public void SpendResources(PriceData priceData)
        {
            _goldAmount -= priceData.GoldAmount;
            _seedAmount -= priceData.SeedAmount;
            
            _progressDataService.SpendResources(_goldAmount,_seedAmount);
        }

        public bool CanAfford(PriceData priceData)
        {
            if (_goldAmount >= priceData.GoldAmount && _seedAmount >= priceData.SeedAmount)
                return true;
            
            return false;
        }

        private void StartingResources()
        {
            _goldAmount = _resourceHolder.StartResource.GoldAmount;
            _progressDataService.GoldenChanged(_goldAmount);
            _seedAmount = _resourceHolder.StartResource.SeedAmount;
            _progressDataService.SeedResourceChanged(_seedAmount);
        }
    }
}