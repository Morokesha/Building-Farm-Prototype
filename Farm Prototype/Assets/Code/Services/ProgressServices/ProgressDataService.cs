using System;
using Code.Services.UpgradeServices;

namespace Code.Services.ProgressServices
{
    public class ProgressDataService : IProgressDataService
    {
        public event Action<int> GoldChanged;
        public event Action<int> SeedChanged;
        public IUpgradeService GetUpgradeService => _upgradeService;

        private int _gold;
        private int _seed;
        private IUpgradeService _upgradeService;


        public void Init(IUpgradeService upgradeService) => 
            _upgradeService = upgradeService;

        public void GoldenChanged(int amount)
        {
            _gold = amount;
            GoldChanged?.Invoke(_gold);
        }

        public void SeedResourceChanged(int amount)
        {
            _seed = amount;
            SeedChanged?.Invoke(_seed);
        }

        public void SpendResources(int gold, int seed)
        {
            _gold = gold;
            GoldChanged?.Invoke(_gold);
            
            _seed = seed;
            SeedChanged?.Invoke(_seed);
        }
    }
}