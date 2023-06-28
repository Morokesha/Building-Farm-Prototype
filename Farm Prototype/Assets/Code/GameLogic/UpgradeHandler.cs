using System;
using Code.Data.UpgradeData;
using Code.Management;
using Code.Services.ShopServices;
using Code.Services.UpgradeServices;
using Code.UI;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.GameLogic
{
    public class  UpgradeHandler: IUpgradeService
    {
        public event Action FirstWateringUpgradeActivated;
        public event Action<ContentItem> SecondWateringUpgradeActivated;
        public event Action FirstHarvestingUpgradeActivated;
        public event Action<ContentItem> SecondHarvestingUpgradeActivated;
        public event Action FirstExpansionUpgradeActivated;
        public event Action<ContentItem> SecondExpansionUpgradeActivated;
        public event Action<ContentItem> ActivatedShovel;
        
        private IShopService _shopService;
        private UpgradeItemData _upgradeItemData;
        private ConstructionBuilder _constructionBuilder;
        
        public void Init(IShopService shopService, ConstructionBuilder constructionBuilder)
        {
            _shopService = shopService;
            _constructionBuilder = constructionBuilder;

            _shopService.SoldUpgrade += OnSoldUpgrade; 
        }

        private void OnSoldUpgrade(UpgradeItemData data,ContentItem contentItem)
        {
            _upgradeItemData = data;

            switch (_upgradeItemData.UpgradeType)
            {
                case UpgradeType.Watering:
                    ActivateWateringUpgrade(contentItem);
                    break;
                case UpgradeType.Harvesting:
                    ActivateHarvestingUpgrade(contentItem);
                    break;
                case UpgradeType.Expansion:
                    ExpandUpgrade(contentItem);
                    break;
                case UpgradeType.Shovel:
                    ActivateShovelUpgrade(contentItem);
                    break;
            }
        }

        private void ActivateWateringUpgrade(ContentItem contentItem)
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    FirstWateringUpgradeActivated?.Invoke();
                    break;
                case UpgradeStage.Second:
                    SecondWateringUpgradeActivated?.Invoke(contentItem);
                    break;
            }
        }

        private void ActivateHarvestingUpgrade(ContentItem contentItem)
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    FirstHarvestingUpgradeActivated?.Invoke();
                    break;
                case UpgradeStage.Second:
                    
                    SecondHarvestingUpgradeActivated?.Invoke(contentItem);
                    break;
            }
        }
        
        private void ExpandUpgrade(ContentItem contentItem)
        {
            switch (_upgradeItemData.UpgradeStage)
            {
                case UpgradeStage.First:
                    _constructionBuilder.AddGridCells(1);
                    FirstExpansionUpgradeActivated?.Invoke();
                    break;
                case UpgradeStage.Second :
                    _constructionBuilder.AddGridCells(2);
                    SecondExpansionUpgradeActivated?.Invoke(contentItem);
                    break;
            }
        }

        private void ActivateShovelUpgrade(ContentItem contentItem) => 
            ActivatedShovel?.Invoke(contentItem);
    }
}