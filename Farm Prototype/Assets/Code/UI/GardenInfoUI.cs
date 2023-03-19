using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.Management;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public class GardenInfoUI : MonoBehaviour
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
        private Button _backBtn;

        [SerializeField] 
        private Image _processingField;

        [SerializeField]
        private TextMeshProUGUI _nameCell;

        [SerializeField] 
        private CanvasGroup _canvasGroup;

        private ConstructionBuilder _constructionBuilder;
        private Garden _garden;

        public void Init(ConstructionBuilder constructionBuilder)
        {
            _constructionBuilder = constructionBuilder;
            _constructionBuilder.SelectedGarden += OnSelectedGarden;
            _backBtn.onClick.AddListener(DeactivateWindow);
            
            DeactivateWindow();
        }

        private void OnSelectedGarden(Garden gardenData)
        {
            _garden = gardenData;
            
            _garden.GetGardenProduction().ActivatedHarvesting += OnActivatedHarvesting;
            _garden.GetGardenProduction().GrowingChanged += OnGrowingChanged;
            _garden.ActivatedWateringBtn += ActiveWateringBtn;

            _nameCell.text = _garden.GetGardenData.GardenName;

            ActivateWindow();
        }

        private void ActiveWateringBtn()
        {
            _wateringBtn.gameObject.SetActive(true);
            _wateringBtn.onClick.AddListener(ActiveGrowing);
        }

        private void OnGrowingChanged(float growing) => 
            _processingField.fillAmount += growing;

        private void ActiveGrowing()
        {
            _garden.SetGardenState(GardenState.Growing);
            HideActionButton(_wateringBtn);
            _wateringBtn.onClick.RemoveListener(ActiveGrowing);
        }

        private void OnActivatedHarvesting(ResourceType type)
        {
            if (type == ResourceType.Gold)
            {
                string goldenHarvestingTxt = _garden.GetGardenData.GeneratorData.CoinAmout +
                                      " COINS READY Click to Harvest!";
                ActivatedHarvestingButton(type,_harvestingGoldBtn, goldenHarvestingTxt);
            }
            else
            {
                string seedHarvestingTxt = _garden.GetGardenData.GeneratorData.SeedAmount +
                                           " SEEDS READY Click to Harvest!";
                ActivatedHarvestingButton(type,_harvestingSeedBtn, seedHarvestingTxt);
            }
        }

        private void ActivatedHarvestingButton(ResourceType type,Button actionBtn, string text)
        {
            actionBtn.gameObject.SetActive(true);
            actionBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
            actionBtn.onClick.AddListener(MakeCollection(type,actionBtn));
        }

        private UnityAction MakeCollection(ResourceType type,Button actionBtn)
        {
            _garden.GetGardenProduction().Harvesting(type);
            HideActionButton(actionBtn);
            return null;
        }


        private void ActivateWindow()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void DeactivateWindow()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            HideActionButton(_wateringBtn);
            HideActionButton(_harvestingGoldBtn);
        }

        private void HideActionButton(Button activeBtn) => 
            activeBtn.gameObject.SetActive(false);
    }
}