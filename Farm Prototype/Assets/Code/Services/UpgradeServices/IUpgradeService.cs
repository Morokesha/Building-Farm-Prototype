using Code.Management;
using Code.Services.ShopServices;
using Code.UI;

namespace Code.Services.UpgradeServices
{
    public interface IUpgradeService
    {
        public bool FirstWateringUpgradeActivated { get; }
        public bool SecondWateringUpgradeActivated { get; }
        public bool FirstHarvestingUpgradeActivated { get; }
        public bool SecondHarvestingUpgradeActivated { get; }
        public bool DemolitionUpgradeActivated { get; }
        
        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder, HUD hud);
    }
}