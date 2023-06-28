using System;
using Code.Management;
using Code.Services.ShopServices;
using Code.UI;
using Code.UI.Windows.Shop.WindowElements;

namespace Code.Services.UpgradeServices
{
    public interface IUpgradeService
    {
        public event Action FirstWateringUpgradeActivated;
        public event Action<ContentItem> SecondHarvestingUpgradeActivated; 
        public event Action FirstHarvestingUpgradeActivated;
        public event Action<ContentItem> SecondWateringUpgradeActivated;
        public event Action FirstExpansionUpgradeActivated;
        public event Action<ContentItem> SecondExpansionUpgradeActivated;
        public event Action<ContentItem> ActivatedShovel;

        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder);
    }
}