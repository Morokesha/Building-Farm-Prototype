using System;
using Code.Data.UpgradeData;
using Code.Management;
using Code.Services.ShopServices;
using Code.Services.UpgradeServices;
using Code.UI;
using UnityEngine;

namespace Code.GameLogic
{
    public class UpgradeHandler: IUpgradeService
    {
        public event Action FirstWateringUpgradeActivated;
        public event Action SecondWateringUpgradeActivated;
        public event Action FirstHarvestingUpgradeActivated;
        public event Action SecondHarvestingUpgradeActivated;

        public bool ShovelUpgradeActivated => _shovelUpgradeActivated;

        private IShopService _shopService;
        private UpgradeItemData _upgradeItemData;
        private ConstructionBuilder _constructionBuilder;
        
        private bool _shovelUpgradeActivated;
        
        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder)
        {
            _shopService = shopService;
            _constructionBuilder = constructionBuilder;

            _shopService.SoldUpgrade += OnSoldUpgrade; 
        }

        private void OnSoldUpgrade(UpgradeItemData data)
        {
            _upgradeItemData = data;

            switch (_upgradeItemData.UpgradeType)
            {
                case UpgradeType.Watering:
                    ActivateWateringUpgrade();
                    break;
                case UpgradeType.Harvesting:
                    ActivateHarvestingUpgrade();
                    break;
                case UpgradeType.Expansion:
                    ExpandUpgrade();
                    break;
                case UpgradeType.Shovel:
                    ActivateShovelUpgrade();
                    break;
            }
        }

        private void ActivateWateringUpgrade()
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    FirstWateringUpgradeActivated!.Invoke();
                    break;
                case UpgradeStage.Second:
                    SecondWateringUpgradeActivated?.Invoke();
                    break;
            }
        }

        private void ActivateHarvestingUpgrade()
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    FirstHarvestingUpgradeActivated?.Invoke();
                    break;
                case UpgradeStage.Second:
                    
                    SecondHarvestingUpgradeActivated?.Invoke();
                    break;
            }
        }
        
        private void ExpandUpgrade()
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First :
                    _constructionBuilder.AddGridCells(1);
                    break;
                case UpgradeStage.Second :
                    _constructionBuilder.AddGridCells(1);
                    break;
                case UpgradeStage.Third:
                    _constructionBuilder.AddGridCells(2);
                    break;
            }
        }

        private void ActivateShovelUpgrade() => 
            _shovelUpgradeActivated = true;
    }
}