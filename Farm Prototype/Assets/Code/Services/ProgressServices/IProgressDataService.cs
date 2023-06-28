﻿using System;
using Code.Services.UpgradeServices;

namespace Code.Services.ProgressServices
{
    public interface IProgressDataService
    {
        public event Action<int> GoldChanged; 
        public event Action<int> SeedChanged;
        
        public bool InteractionWateringActivated { get; }
        public bool InteractionHarvestingActivated { get; }
        public  bool ShovelActivated { get; }
        
        public IUpgradeService GetUpgradeService { get; }
        void Init(IUpgradeService upgradeService);
        void GoldenChanged(int amount);
        void SeedResourceChanged(int amount);
        void SpendResources(int gold, int seed);
    }
}