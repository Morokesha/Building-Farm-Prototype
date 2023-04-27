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
        public bool FirstWateringUpgradeActivated => _firstWateringUpdate;
        public bool SecondWateringUpgradeActivated => _secondWateringUpdate;
        public bool FirstHarvestingUpgradeActivated => _firstHarvestingUpgrade;
        public bool SecondHarvestingUpgradeActivated => _secondHarvestingUpgrade;
        public bool DemolitionUpgradeActivated => _demolitionUpgradeActivated;

        private IShopService _shopService;
        private UpgradeItemData _upgradeItemData;
        private ConstructionBuilder _constructionBuilder;
        private HUD _hud;

        private bool _firstWateringUpdate;
        private bool _secondWateringUpdate;
        private bool _firstHarvestingUpgrade;
        private bool _secondHarvestingUpgrade;
        private bool _demolitionUpgradeActivated;
        
        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder,HUD hud)
        {
            _shopService = shopService;
            _constructionBuilder = constructionBuilder;
            _hud = hud;
            
            _shopService.SoldUpgrade += OnSoldUpgrade; 
        }

        private void OnSoldUpgrade(UpgradeItemData data) => 
            _upgradeItemData = data;

        private void ActivateWateringUpgrade()
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    _firstWateringUpdate = true;
                    break;
                case UpgradeStage.Second:
                    _secondWateringUpdate = true;
                    break;
            }
        }

        private void ActivateHarvestingUpgrade()
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    _firstHarvestingUpgrade = true;
                    break;
                case UpgradeStage.Second:
                    _secondHarvestingUpgrade = true;
                    break;
            }
        }
        
        private void ExpansionTerritory()
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

        private void ActivateDemolitionUpgrade() => 
            _demolitionUpgradeActivated = true;
    }
}