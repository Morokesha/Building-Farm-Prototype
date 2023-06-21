using System;
using Code.Management;
using Code.Services.ShopServices;
using Code.UI;

namespace Code.Services.UpgradeServices
{
    public interface IUpgradeService
    {
        public event Action FirstWateringUpgradeActivated;
        public event Action SecondHarvestingUpgradeActivated; 
        public event Action FirstHarvestingUpgradeActivated;
        public event Action SecondWateringUpgradeActivated;
        
        public bool ShovelUpgradeActivated { get; }
        
        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder);
    }
}