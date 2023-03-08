using System;
using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Management
{
    public class ResourceRepository
    {
        public event Action ResourcesChanged;

        private IProgressDataService _progressDataService;
        private ResourceHolder _resourceHolder;

        private Dictionary<ResourceType, int> _resourceTypeAmountDictionary;

    public void Init(IProgressDataService progressDataService,ResourceHolder resourceHolder)
    {
        _progressDataService = progressDataService;
        _resourceHolder = resourceHolder;
        _resourceTypeAmountDictionary = new Dictionary<ResourceType, int>();

        StartingResources();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        _resourceTypeAmountDictionary[resourceType] += amount;

        int value = _resourceTypeAmountDictionary[resourceType];
        
        _progressDataService.AddResources(resourceType,value);
        ResourcesChanged?.Invoke();
    }

    public void SpendResources(ResourceAmountData[] resourceAmountArray)
    {
        foreach (ResourceAmountData resourceAmount in resourceAmountArray)
            _resourceTypeAmountDictionary[resourceAmount.ResourceData.Type] -= resourceAmount.Amount;
        
        ResourcesChanged?.Invoke();
    }

    public bool CanAfford(ResourceAmountData[] resourceAmountArray)
    {
        foreach (ResourceAmountData resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.ResourceData) >= resourceAmount.Amount)
            {
                //могу купить
            }
            else
            {
                return false;
            }
        }
        
        return true;
    }

    private void StartingResources()
    {
        foreach (var resourceAmountData in _resourceHolder.ResourceAmounts)
            _resourceTypeAmountDictionary.Add(resourceAmountData.ResourceData.Type, resourceAmountData.Amount);
    }


    private int GetResourceAmount(ResourceData resource) =>
        _resourceTypeAmountDictionary[resource.Type];
    }
}