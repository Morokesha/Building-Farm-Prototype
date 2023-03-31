using Code.Common;
using Code.GameLogic.Gardens;
using Code.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaTab
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
            _constructionBuilder = constructionBuilder;
            _constructionBuilder.SelectedGarden += OnSelectedGarden;
            _interactiveButtons.Init();
            
            _backBtn.onClick.AddListener(HideWindow);
            
            HideWindow();
        }

        public void HideWindow()
        {
            _canvasGroup.SetActive(false);
            _constructionBuilder.ClearSelectedGarden();
        }

        private void OnSelectedGarden(Garden garden)
        {
            Show();
            
            _garden = garden;
            _nameCell.text = _garden.GetGardenData.CropsShopData.NameItem;
            _interactiveButtons.SetGardenProduction(garden.GetGardenProduction());
            _processBar.UpdateProgressBar(_garden.GetGardenProduction());
        }

        private void Show() => 
            _canvasGroup.SetActive(true);

        private void OnDestroy()
        {
            _constructionBuilder.SelectedGarden -= OnSelectedGarden;
            _backBtn.onClick.RemoveListener(HideWindow);
        }
    }
}