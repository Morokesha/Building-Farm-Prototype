﻿using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaTab
{
    public class GardenInteractiveButtons : MonoBehaviour
    {
        [SerializeField]
        private Button _wateringBtn;
        [SerializeField]
        private Button _harvestingGoldBtn;
        [SerializeField]
        private Button _harvestingSeedBtn;
        [SerializeField] 
        private Button _removeBtn;

        [SerializeField] 
        private TextMeshProUGUI _goldText;
        [SerializeField] 
        private TextMeshProUGUI _seedText;
        [SerializeField] 
        private TextMeshProUGUI _growingText;
        
        private GardenProduction _gardenProduction;

        public void Init()
        {
            _wateringBtn.onClick.AddListener(OnClickWatering);
            _harvestingGoldBtn.onClick.AddListener(OnClickGold);
            _harvestingSeedBtn.onClick.AddListener(OnClickSeed);
        }

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
        }

        private void OnClickGold()
        {
            _gardenProduction.Harvesting(ResourceType.Gold);
            Hide(_harvestingGoldBtn);
        }

        private void OnClickSeed()
        {
            _gardenProduction.Harvesting(ResourceType.Seed);
            Hide(_harvestingSeedBtn);
        }

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
                    break;
            }
        }

        private void ActivatedHarvestingBtn(ResourceType type)
        {
            if (type == ResourceType.Gold)
            {
                _goldText.SetText(_gardenProduction.GetGardenData().GeneratorData.CoinAmout +
                                  " COINS READY Click to Harvest!");
                _harvestingGoldBtn.gameObject.SetActive(true);
            }
            if(type == ResourceType.Seed)
            {
                _seedText.SetText(_gardenProduction.GetGardenData().GeneratorData.SeedAmount +
                                  " SEEDS READY Click to Harvest!");
                _harvestingSeedBtn.gameObject.SetActive(true);
            }
        }

        private void Show(Button button)
        {
            button.gameObject.SetActive(true);
        }
        private void Hide(Button button)
        {
            button.gameObject.SetActive(false);
        }

        private void HideAllInteraction()
        {
            Hide(_wateringBtn);
            Hide(_harvestingGoldBtn);
            Hide(_harvestingSeedBtn);
            _growingText.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _wateringBtn.onClick.RemoveListener(OnClickWatering);
            _harvestingGoldBtn.onClick.RemoveListener(OnClickGold);
            _harvestingSeedBtn.onClick.RemoveListener(OnClickSeed);
        }
    }
}