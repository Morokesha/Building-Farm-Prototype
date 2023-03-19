using System.Collections.Generic;
using Code.Data.ResourceData;

namespace Code.Services
{
    public class ProgressDataService : IProgressDataService
    {
        private int _coin;
        private int _seed;

        private Dictionary<ResourceType, int> _resourceAmountDictionary = new Dictionary<ResourceType, int>();

        public void AddResources(ResourceType type, int amount)
        {
            _resourceAmountDictionary.Add(type,amount);

            _coin = _resourceAmountDictionary[ResourceType.Gold];
            _seed = _resourceAmountDictionary[ResourceType.Seed];
        }

        public int GetCoinCount() => 
            _coin;
        public int GetSeedCount() =>
            _seed;
    }
}