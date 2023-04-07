using System;

namespace Code.Services.ProgressServices
{
    public class ProgressDataService : IProgressDataService
    {
        public event Action<int> GoldChanged;
        public event Action<int> SeedChanged;
        
        private int _gold;
        private int _seed;

        public void AddGold(int amount)
        {
            _gold += amount;
            GoldChanged?.Invoke(_gold);
        }

        public void AddSeed(int amount)
        {
            _seed += amount;
            SeedChanged?.Invoke(_seed);
        }

        public void SpendResources(int gold, int seed)
        {
            _gold -= gold;
            GoldChanged?.Invoke(_gold);
            
            _seed -= seed;
            SeedChanged?.Invoke(_seed);
        }
    }
}