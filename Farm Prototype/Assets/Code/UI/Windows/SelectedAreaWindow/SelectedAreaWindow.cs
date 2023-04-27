using Code.Common;
using Code.GameLogic.Gardens;
using Code.Management;
using Code.Services.GardenHandlerService;
using Code.UI.Windows.SelectedAreaWindow.WindowElements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaWindow
{
    public class SelectedAreaWindow : MonoBehaviour
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

        private IGardenHandlerService _gardenHandlerService;
        private ConstructionBuilder _constructionBuilder;
        private Garden _garden;

        public void Init(IGardenHandlerService gardenHandlerService,ConstructionBuilder constructionBuilder)
        {
            _gardenHandlerService = gardenHandlerService;
            _constructionBuilder = constructionBuilder;
            _constructionBuilder.SelectedGarden += OnSelectedGarden;
            _interactiveButtons.Init();
            _interactiveButtons.DemolitionGardenActivated += RemoveGarden;
            
            _backBtn.onClick.AddListener(HideWindow);
            
            HideWindow();
        }

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
            _constructionBuilder.ClearSelectedGarden();
        }

        private void OnDestroy()
        {
            _constructionBuilder.SelectedGarden -= OnSelectedGarden;
            _backBtn.onClick.RemoveListener(HideWindow);
        }
    }
}