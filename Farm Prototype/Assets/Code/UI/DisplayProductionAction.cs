using System;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class DisplayProductionAction : MonoBehaviour
    {
        [SerializeField] 
        private Garden _garden;
        [SerializeField] 
        private Button _wateringBtn;
        [SerializeField] 
        private Button _harvestingGoldBtn;
        [SerializeField]
        private Button _harvestingSeedBtn;
        
        private GardenProduction _gardenProduction;

        private void Start()
        {
            _gardenProduction = _garden.GetGardenProduction();
            _gardenProduction.ProductionStateChanged += OnProductionStateChanged;
            
            _wateringBtn.onClick.AddListener(OnWateringClick);
            _harvestingGoldBtn.onClick.AddListener(OnHarvestingGoldClick);
            _harvestingSeedBtn.onClick.AddListener(OnHarvestingSeedClick);
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

        private void OnProductionStateChanged(ProductionState productionState)
        {
            switch (productionState)
            {
                case ProductionState.WaitWatering:
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
            }
        }

        private void ActiveButton(Button button,bool active) => 
            button.gameObject.SetActive(active);
    }
}