using System;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.Windows.GardenIfoTab
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
        
        private Garden _garden;

        private void Init(Garden garden)
        {
            _garden = garden;
            switch (_garden.GetGardenState())
            {
                case GardenState.WaitWatering:
                    Show(_wateringBtn);
                    _wateringBtn.onClick.AddListener(ActiveGrowing);
                    break;
                case GardenState.Growing:
                    _growingText.gameObject.SetActive(true);
                    break;
                case GardenState.CompleteGrowth:
                    _garden.GetGardenProduction().ActivatedHarvesting += OnActivatedHarvesting;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void ActiveGrowing()
        {
            _garden.GetGardenProduction().Growing();

            _wateringBtn.onClick.RemoveListener(ActiveGrowing);
        }

        private void OnActivatedHarvesting(ResourceType type)
        {
            if (type == ResourceType.Gold)
            {
                _goldText.SetText(_garden.GetGardenData.GeneratorData.CoinAmout +
                                  " COINS READY Click to Harvest!");
                _harvestingGoldBtn.gameObject.SetActive(true);
            }
            if(type == ResourceType.Seed)
            {
                _seedText.SetText(_garden.GetGardenData.GeneratorData.SeedAmount +
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
    }
}