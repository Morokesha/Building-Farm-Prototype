using System;
using Code.Data.ResourceData;

namespace Code.Services
{
    public interface IResourceService
    {
        void Init(IProgressDataService progressDataService,ResourceHolder resourceHolder);
        void AddResource(ResourceType resourceType, int amount);
        void SpendResources(ResourceAmountData[] resourceAmountArray);
        bool CanAfford(ResourceAmountData[] resourceAmountArray);
    }
}