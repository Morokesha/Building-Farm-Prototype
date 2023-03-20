using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.Management;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI.Windows.GardenIfoTab
{
    public class SelectedGardenWindow : MonoBehaviour
    {
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
            
            _backBtn.onClick.AddListener(HideWindow);
        }

        private void OnSelectedGarden(Garden gardenData)
        {
            _garden = gardenData;

            _nameCell.text = _garden.GetGardenData.GardenName;
        }

        

        private void HideWindow() => _canvasGroup.gameObject.SetActive(false);
    }
}