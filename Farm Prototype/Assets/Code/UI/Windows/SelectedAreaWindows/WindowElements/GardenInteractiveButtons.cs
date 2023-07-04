using System;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.Services.ProgressServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaWindows.WindowElements
{
    public class GardenInteractiveButtons : MonoBehaviour
    {
        public event Action DemolitionGardenActivated;
        public event Action FirstWatering;
        public event Action FirstHarvesting;
        public event Action GrowingCompleted;

        [SerializeField]
        private Button _wateringBtn;
        [SerializeField]
        private Button _harvestingGoldBtn;
        [SerializeField]
        private Button _harvestingSeedBtn;
        [SerializeField] 
        private Button _shovelBtn;

        [SerializeField] 
        private TextMeshProUGUI _goldText;
        [SerializeField] 
        private TextMeshProUGUI _seedText;
        [SerializeField] 
        private TextMeshProUGUI _growingText;
        
        private IProgressService _progressService;
        private GardenProduction _gardenProduction;

        public void Init(IProgressService progressService)
        {
            _progressService = progressService;
            _progressService.ShovelActivated += OnShovelActivated;
            
            _wateringBtn.onClick.AddListener(OnClickWatering);
            _harvestingGoldBtn.onClick.AddListener(OnClickGold);
            _harvestingSeedBtn.onClick.AddListener(OnClickSeed);
            _shovelBtn.onClick.AddListener(OnClickDemolition);
            
            CheckShovelUpgrade();
        }

        private void OnShovelActivated() => 
            CheckShovelUpgrade();

        public void SetGardenProduction(GardenProduction gardenProduction)
        {
            if (_gardenProduction != null)
            {
                if (_gardenProduction == gardenProduction)
                    return;
                _gardenProduction.ProductionStateChanged -= OnGardenProductionChangedState;
            }
            
            _gardenProduction = gardenProduction;
            _gardenProduction.ProductionStateChanged += OnGardenProductionChangedState;
            
            OnGardenProductionChangedState(_gardenProduction.GetProductionState());
        }

        private void OnClickWatering()
        {
            _gardenProduction.Growing();
            Hide(_wateringBtn);
            
            FirstWatering?.Invoke();
        }

        private void OnClickGold()
        {
            _gardenProduction.Harvesting(ResourceType.Gold);
            FirstHarvesting?.Invoke();
            Hide(_harvestingGoldBtn);
        }

        private void OnClickSeed()
        {
            _gardenProduction.Harvesting(ResourceType.Seed);
            FirstHarvesting?.Invoke();
            Hide(_harvestingSeedBtn);
        }
        
        private void OnClickDemolition() => 
            DemolitionGardenActivated?.Invoke();

        private void OnGardenProductionChangedState(ProductionState state)
        {
            HideAllInteraction();

            switch (state)
            {
                case ProductionState.WaitWatering:
                    Show(_wateringBtn);
                    break;
                case ProductionState.Growing:
                    _growingText.gameObject.SetActive(true);
                    break;
                case ProductionState.CompleteGrowth:
                    ResourceType resourceType = _gardenProduction.GetHarvestingResourceType();
                    ActivatedHarvestingBtn(resourceType);
                    GrowingCompleted?.Invoke();
                    break;
            }
        }

        private void ActivatedHarvestingBtn(ResourceType type)
        {
            if (type == ResourceType.Gold)
            {
                _goldText.SetText(_gardenProduction.GetGardenData().DropData.GoldAmount +
                                  " GOLD READY Click to Harvest!");
                _harvestingGoldBtn.gameObject.SetActive(true);
            }
            if(type == ResourceType.Seed)
            {
                _seedText.SetText(_gardenProduction.GetGardenData().DropData.SeedAmount +
                                  " SEEDS READY Click to Harvest!");
                _harvestingSeedBtn.gameObject.SetActive(true);
            }
        }

        private void Show(Button button) => 
            button.gameObject.SetActive(true);

        private void Hide(Button button) => 
            button.gameObject.SetActive(false);

        private void HideAllInteraction()
        {
            Hide(_wateringBtn);
            Hide(_harvestingGoldBtn);
            Hide(_harvestingSeedBtn);
            _growingText.gameObject.SetActive(false);
        }

        private void CheckShovelUpgrade()
        {
            if (_progressService.ShovelIsActivated == true) 
                Show(_shovelBtn);
            else
                Hide(_shovelBtn);
        }

        private void OnDestroy()
        {
            _wateringBtn.onClick.RemoveListener(OnClickWatering);
            _harvestingGoldBtn.onClick.RemoveListener(OnClickGold);
            _harvestingSeedBtn.onClick.RemoveListener(OnClickSeed);
            _shovelBtn.onClick.RemoveListener(OnClickDemolition);

            _progressService.ShovelActivated -= OnShovelActivated;
        }
    }
}