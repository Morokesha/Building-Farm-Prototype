using System;
using Code.Data.ShopData.UpgradeData;
using Code.Services.ShopServices;
using Code.Services.UpgradeServices;

namespace Code.GameLogic
{
    public class UpgradeHandler: IUpgradeService
    {
        private bool _firstWateringUpdate;
        private bool _secondWateringUpdate;
        private IShopService _shopService;
        private UpgradeItemData _upgradeItemData;

        public void Init(IShopService shopService)
        {
            _shopService = shopService;
            _shopService.SoldUpgrade += OnSoldUpgrade; 
        }

        private void OnSoldUpgrade(UpgradeItemData data)
        {
            _upgradeItemData = data;
        }

        private void ActivateUpgrade()
        {
            switch (_upgradeItemData.UpgradeType)
            {
                case UpgradeType.Watering:
                    break;
                case UpgradeType.Harvesting:
                    break;
                case UpgradeType.Expansion:
                    break;
                case UpgradeType.Demolition:
                    break;
            }
        }
    }
}