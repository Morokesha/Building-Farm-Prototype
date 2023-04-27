using System;
using Code.Services.UpgradeServices;

namespace Code.Services.ProgressServices
{
    public interface IProgressDataService
    {
        public event Action<int> GoldChanged; 
        public event Action<int> SeedChanged;
        IUpgradeService GetUpgradeService { get;}
        void Init(IUpgradeService upgradeService);
        void AddGold(int amount);
        void AddSeed(int amount);
        void SpendResources(int gold, int seed);
    }
}