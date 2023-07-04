using System;
using Code.Common;
using Code.GameLogic.Gardens;
using Code.Management;
using Code.Services.GardenHandlerService;
using Code.Services.ProgressServices;
using Code.UI.Windows.SelectedAreaWindows.WindowElements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaWindows
{
    public class SelectedAreaWindow : MonoBehaviour
    {
        public event Action FirstWatering;
        public event Action FirstHarvesting;
        public event Action GrowingCompleted;
        
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

        private IGardenHandlerService _gardenHandlerService;
        private IProgressService _progressService;
        private FarmController _farmController;
        private Garden _garden;

        public void Init(IGardenHandlerService gardenHandlerService,IProgressService progressService,
            FarmController farmController)
        {
            _gardenHandlerService = gardenHandlerService;
            _progressService = progressService;
            _farmController = farmController;
            _farmController.SelectedGarden += OnSelectedGarden;
            
            _interactiveButtons.Init(_progressService);
            _interactiveButtons.FirstWatering += OnFirstWatering;
            _interactiveButtons.FirstHarvesting += OnFirstHarvesting;
            _interactiveButtons.GrowingCompleted += OnGrowingCompleted;
            _interactiveButtons.DemolitionGardenActivated += RemoveGarden;
            
            _backBtn.onClick.AddListener(HideWindow);
            
            HideWindow();
        }

        private void OnGrowingCompleted() => 
            GrowingCompleted?.Invoke();

        private void OnFirstHarvesting() => 
            FirstHarvesting?.Invoke();

        private void OnFirstWatering() => 
            FirstWatering?.Invoke();

        private void OnSelectedGarden(Garden garden)
        {
            Show();
            
            _garden = garden;
            _nameCell.text = _garden.GetGardenData.NameGarden;
            _interactiveButtons.SetGardenProduction(garden.GetGardenProduction());
            _processBar.UpdateProgressBar(_garden.GetGardenProduction());
        }

        private void RemoveGarden()
        {
            _gardenHandlerService.RemoveGarden(_garden);
            Destroy(_garden.gameObject);
        }

        private void Show() => 
            _canvasGroup.SetActive(true);

        public void HideWindow()
        {
            _canvasGroup.SetActive(false);
            _farmController.ClearSelectedGarden();
        }

        private void OnDestroy()
        {
            _farmController.SelectedGarden -= OnSelectedGarden;
            _interactiveButtons.FirstWatering -= OnFirstWatering;
            _interactiveButtons.FirstHarvesting -= OnFirstHarvesting;
            _interactiveButtons.GrowingCompleted -= OnGrowingCompleted;
            _interactiveButtons.DemolitionGardenActivated -= RemoveGarden;
            _backBtn.onClick.RemoveListener(HideWindow);
        }
    }
}