using System;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.Services.ProgressServices;
using Code.Services.UpgradeServices;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class DisplayProductionAction : MonoBehaviour
    {
        [SerializeField] 
        private Button _wateringBtn;
        [SerializeField] 
        private Button _harvestingGoldBtn;
        [SerializeField]
        private Button _harvestingSeedBtn;
        
        private IProgressDataService _progressService;
        private GardenProduction _gardenProduction;
        
        public void Init(IProgressDataService progressService,GardenProduction gardenProduction)
        {
            _progressService = progressService;
            _gardenProduction = gardenProduction;
            
            _gardenProduction.ProductionStateChanged += OnProductionStateChanged;
            _progressService.GetUpgradeService.FirstWateringUpgradeActivated += ActiveWateringInteraction;
            _progressService.GetUpgradeService.FirstHarvestingUpgradeActivated += ActiveHarvestingInteraction;

            _wateringBtn.onClick.AddListener(OnWateringClick);
            _harvestingGoldBtn.onClick.AddListener(OnHarvestingGoldClick);
            _harvestingSeedBtn.onClick.AddListener(OnHarvestingSeedClick);
            
            CheckUpgrade();
        }

        private void ActiveHarvestingInteraction()
        {
            ActivateBtnInteraction(_harvestingSeedBtn);
            ActivateBtnInteraction(_harvestingGoldBtn);
        }

        private void ActiveWateringInteraction() => 
            ActivateBtnInteraction(_wateringBtn);

        private void OnProductionStateChanged(ProductionState productionState)
        {
            switch (productionState)
            {
                case ProductionState.WaitWatering:
                    ActiveButton(_harvestingGoldBtn,false);
                    ActiveButton(_harvestingSeedBtn,false);
                    ActiveButton(_wateringBtn, true);
                    
                    break;
                case ProductionState.CompleteGrowth:
                { 
                    if (_gardenProduction.GetHarvestingResourceType() == ResourceType.Gold)
                        ActiveButton(_harvestingGoldBtn, true);
                    if (_gardenProduction.GetHarvestingResourceType() == ResourceType.Seed)
                        ActiveButton(_harvestingSeedBtn, true);
                    break;
                }
                case ProductionState.Growing:
                    HideAllBtn();
                    break;
            }
        }

        private void OnHarvestingSeedClick()
        {
            _gardenProduction.Harvesting(ResourceType.Seed);
            ActiveButton(_harvestingSeedBtn, false);
        }

        private void OnHarvestingGoldClick()
        {
            _gardenProduction.Harvesting(ResourceType.Gold);
            ActiveButton(_harvestingGoldBtn, false);
        }

        private void OnWateringClick()
        {
            _gardenProduction.Growing();
            ActiveButton(_wateringBtn, false);
        }

        private void CheckUpgrade()
        {
            if (_progressService.InteractionWateringActivated) 
                ActivateBtnInteraction(_wateringBtn);
            if (_progressService.InteractionHarvestingActivated)
            {
                ActivateBtnInteraction(_harvestingGoldBtn);
                ActivateBtnInteraction(_harvestingSeedBtn);
            }
        }

        private void ActiveButton(Button button,bool active) => 
            button.gameObject.SetActive(active);

        private void ActivateBtnInteraction(Button button) => 
            button.interactable = true;

        private void HideAllBtn()
        {
            ActiveButton(_wateringBtn,false);
            ActiveButton(_harvestingSeedBtn,false);
            ActiveButton(_harvestingGoldBtn,false);
        }
        private void OnDestroy()
        {
            _wateringBtn.onClick.RemoveListener(OnWateringClick);
            _harvestingGoldBtn.onClick.RemoveListener(OnHarvestingGoldClick);
            _harvestingSeedBtn.onClick.RemoveListener(OnHarvestingSeedClick);
            
            _gardenProduction.ProductionStateChanged -= OnProductionStateChanged;
            _progressService.GetUpgradeService.FirstHarvestingUpgradeActivated -= ActiveWateringInteraction;
            _progressService.GetUpgradeService.FirstWateringUpgradeActivated -= ActiveHarvestingInteraction;
        }
    }
}