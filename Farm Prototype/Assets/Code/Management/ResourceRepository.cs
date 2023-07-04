using Code.Data.ResourceData;
using Code.Data.ShopData;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;

namespace Code.Management
{
    public class ResourceRepository : IResourceService
    {
        private IProgressService _progressService;
        private ResourceHolder _resourceHolder;

        private int _goldAmount;
        private int _seedAmount;

        public void Init(IProgressService progressService, ResourceHolder resourceHolder)
        {
            _progressService = progressService;
            _resourceHolder = resourceHolder;

            StartingResources();
        }

        public void AddGold(DropData dropData)
        {
            _goldAmount += dropData.GoldAmount;
            _progressService.GoldenChanged(_goldAmount);
        }

        public void AddSeed(DropData dropData)
        {
            _seedAmount += dropData.SeedAmount;
            _progressService.SeedResourceChanged(_seedAmount);
        }

        public void SpendResources(PriceData priceData)
        {
            _goldAmount -= priceData.GoldAmount;
            _seedAmount -= priceData.SeedAmount;
            
            _progressService.SpendResources(_goldAmount,_seedAmount);
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
            _progressService.GoldenChanged(_goldAmount);
            _seedAmount = _resourceHolder.StartResource.SeedAmount;
            _progressService.SeedResourceChanged(_seedAmount);
        }
    }
}