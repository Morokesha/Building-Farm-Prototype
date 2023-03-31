using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _goldTxt;
        [SerializeField]
        private TextMeshProUGUI _seedTxt;
        
        [SerializeField] 
        private Button _shopMenuBtn;

        private ShopUI _shopUI;
        private SelectedGardenWindow _selectedGardenWindow;

        private IProgressDataService _progressDataService;

        private ResourceHolder _resourceHolder;

        private Dictionary<ResourceType, Transform> _resourceTypeTransforms;

        private Transform _parentForResourceUI;

        public void Init(IProgressDataService progressDataService,ShopUI shopUI,
        SelectedGardenWindow selectedGardenWindow)
        {
            _shopUI = shopUI;
            _selectedGardenWindow = selectedGardenWindow;
            _progressDataService = progressDataService;
            _progressDataService.ResourceChanded += ResourceUpdater;
        }

        private void Start() => 
            _shopMenuBtn.onClick.AddListener(ShowShopMenu);

        private void ShowShopMenu()
        {
            _shopUI.ActivatedShopMenu();
            _selectedGardenWindow.HideWindow();
        }


        private void ResourceUpdater(ResourceType type, int amount)
        {
            if (type == ResourceType.Gold) 
                _goldTxt.SetText(amount.ToString());
            else
                _seedTxt.SetText(amount.ToString());
        }

        private void OnDestroy() => 
            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
    }
}