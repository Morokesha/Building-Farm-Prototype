using System;
using Code.Services.UpgradeServices;
using Code.UI.Windows.Shop.WindowElements;

namespace Code.Services.ProgressServices
{
    public class ProgressService : IProgressService
    {
        public event Action<int> GoldChanged;
        public event Action<int> SeedChanged;

        public event Action ShovelActivated;
        public event Action<bool> SickleActivated;
        public IUpgradeService GetUpgradeService => _upgradeService;
        private IUpgradeService _upgradeService;
        
        public bool InteractionWateringActivated => _interactionWateringActivated;
        public bool InteractionHarvestingActivated => _interactionHarvestingActivated;

        public bool ShovelIsActivated => _shovelIsActivated;

        private bool _shovelIsActivated = false;
        private bool _interactionWateringActivated = false;
        private bool _interactionHarvestingActivated = false;
        
        private int _gold;
        private int _seed;

        public void Init(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
            _upgradeService.FirstWateringUpgradeActivated += OnFirstWateringUpgradeActivated;
            _upgradeService.FirstHarvestingUpgradeActivated += OnFirstHarvestingUpgradeActivated;
            _upgradeService.ActivatedShovel += OnActivatedShovel;
        }

        private void OnActivatedShovel(ContentItem contentItem)
        {
            _shovelIsActivated = true;
            ShovelActivated?.Invoke();
        }

        private void OnFirstHarvestingUpgradeActivated()
        {
            _interactionHarvestingActivated = true;
            SickleActivated?.Invoke(_interactionHarvestingActivated);
        }

        private void OnFirstWateringUpgradeActivated() => 
            _interactionWateringActivated = true;

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