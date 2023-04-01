using System;
using Code.Data.ResourceData;

namespace Code.Services
{
    public interface IProgressDataService
    {
        public event Action<int> GoldChanged; 
        public event Action<int> SeedChanged; 
        void AddGold(int amount);
        void AddSeed(int amount);
        void SpendResources(int gold, int seed);
    }
}