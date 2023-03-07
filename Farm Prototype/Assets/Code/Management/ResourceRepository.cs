using System;
using System.Collections.Generic;
using Code.Data.ResourceData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Management
{
    public class ResourceRepository : MonoBehaviour
    {
        public event Action<ResourceType,int> ResourcesChanged;
        
        [SerializeField] 
        private ResourceHolder _resourceHolder;
        
        private Dictionary<ResourceType, int> _resourceTypeAmountDictionary;

    private void Start()
    {
        _resourceTypeAmountDictionary = new Dictionary<ResourceType, int>();

        StartingResources();
    }
   private void StartingResources()
    {
        foreach (var resourceAmount in _resourceHolder.ResourceAmounts)
            _resourceTypeAmountDictionary[resourceAmount.ResourceData.Type] = 0;

        foreach (var holder in _resourceHolder.ResourceAmounts)
        {
            AddResource(_resourceHolder.ResourceAmounts, holder.Amount);
        }
    }
 

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
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

    public void AddResource(ResourceAmount[] resourceAmounts, int amount)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            if (resourceAmount.ResourceData.Type == ResourceType.Seed)
            {
                _resourceTypeAmountDictionary[resourceAmount.ResourceData.Type] += amount;
            }
            else if (resourceAmount.ResourceData.Type == ResourceType.Coin)
            {
                _resourceTypeAmountDictionary[resourceAmount.ResourceData.Type] += amount;
            }
        }
        
        foreach (var resourceDictionary in _resourceTypeAmountDictionary)
            ResourcesChanged?.Invoke(resourceDictionary.Key, resourceDictionary.Value);
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
            _resourceTypeAmountDictionary[resourceAmount.ResourceData.Type] -= resourceAmount.Amount;
        
        foreach (var resourceDictionary in _resourceTypeAmountDictionary)
            ResourcesChanged?.Invoke(resourceDictionary.Key, resourceDictionary.Value);
    }

    private int GetResourceAmount(ResourceData resource) =>
        _resourceTypeAmountDictionary[resource.Type];
    }
}