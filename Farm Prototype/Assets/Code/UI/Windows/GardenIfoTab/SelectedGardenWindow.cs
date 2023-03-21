using System;
using Code.Common;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.Management;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI.Windows.GardenIfoTab
{
    public class SelectedGardenWindow : MonoBehaviour
    {
        [SerializeField]
        private GardenInteractiveButtons _interactiveButtons;
        [SerializeField] 
        private GrowingProgressBar _processBar;
        [SerializeField] 
        private Button _backBtn;
        
        [SerializeField]
        private TextMeshProUGUI _nameCell;
        
        [SerializeField] 
        private CanvasGroup _canvasGroup;
        
        private ConstructionBuilder _constructionBuilder;
        private Garden _garden;
        
        public void Init(ConstructionBuilder constructionBuilder)
        {
            Hide();
            
            _constructionBuilder = constructionBuilder;
            _constructionBuilder.SelectedGarden += OnSelectedGarden;
            _interactiveButtons.Init();
            
            _backBtn.onClick.AddListener(HideWindow);
        }

        private void OnSelectedGarden(Garden garden)
        {
            Show();
            
            _garden = garden;
            _nameCell.text = _garden.GetGardenData.GardenName;
            _interactiveButtons.SetGardenProduction(garden.GetGardenProduction());
            _processBar.Init(_garden.GetGardenProduction());
        }

        private void Show()
        {
            _canvasGroup.SetActive(true);
        }
        
        private void HideWindow()
        {
            _constructionBuilder.ClearSelectedGarden();
            Hide();
        }

        private void Hide()
        {
            _canvasGroup.SetActive(false);
        }

        private void OnDestroy()
        {
            _constructionBuilder.SelectedGarden -= OnSelectedGarden;
            _backBtn.onClick.RemoveListener(HideWindow);
        }
    }
}