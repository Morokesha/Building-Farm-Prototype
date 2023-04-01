using System;
using Code.Data.ResourceData;
using Code.Data.ShopData;

namespace Code.Services
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