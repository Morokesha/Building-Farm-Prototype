using System;
using System.Collections.Generic;
using Code.Data.ResourceData;

namespace Code.Services
{
    public class ProgressDataService : IProgressDataService
    {
        public event Action<ResourceType, int> ResourceChanded;
        
        private int _gold;
        private int _seed;

        private Dictionary<ResourceType, int> _resourceAmountDictionary;
        
        public ProgressDataService()
        {
            _resourceAmountDictionary = new Dictionary<ResourceType, int>();
            
            _resourceAmountDictionary.Add(ResourceType.Gold,_gold);
            _resourceAmountDictionary.Add(ResourceType.Seed,_seed);
        }

        public void AddResources(ResourceType type, int amount)
        {
            _resourceAmountDictionary[type] += amount;
            
            ResourceChanded?.Invoke(type, amount);
        }
    }
}