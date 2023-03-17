using System;
using Code.Data.GardenBedData;
using Code.GameLogic.Gardens;
using Code.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GardenUI
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
        private Button BackBtn;

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
            
            DeactivateWindow();
        }

        private void OnSelectedGarden(Garden gardenData)
        {
            _garden = gardenData;

            _nameCell.text = _garden.GetGardenData.GardenName;

            ActivateWindow();
        }

        private void OnGrowingCompleted()
        {
            
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
        }
    }
}