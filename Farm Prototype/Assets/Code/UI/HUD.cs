using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services;
using Code.Services.ProgressServices;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
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

        private ShopWindow _shopWindow;
        private SelectedAreaWindow _selectedAreaWindow;

        private IProgressDataService _progressDataService;

        private ResourceHolder _resourceHolder;

        private Dictionary<ResourceType, Transform> _resourceTypeTransforms;

        private Transform _parentForResourceUI;

        public void Init(IProgressDataService progressDataService,ShopWindow shopWindow,
        SelectedAreaWindow selectedAreaWindow)
        {
            _shopWindow = shopWindow;
            _selectedAreaWindow = selectedAreaWindow;
            _progressDataService = progressDataService;
            
            _progressDataService.GoldChanged += OnGoldChanged;
            _progressDataService.SeedChanged += OnSeedChanged;
        }

        private void OnGoldChanged(int amount) => 
            _goldTxt.SetText(amount.ToString());

        private void OnSeedChanged(int amount) => 
            _seedTxt.SetText(amount.ToString());

        private void Start() => 
            _shopMenuBtn.onClick.AddListener(ShowShopMenu);

        private void ShowShopMenu()
        {
            _shopWindow.ActivatedShopMenu();
            _selectedAreaWindow.HideWindow();
        }
        

        private void OnDestroy()
        {
            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
            
            _progressDataService.GoldChanged -= OnGoldChanged;
            _progressDataService.SeedChanged -= OnSeedChanged;
        }
    }
}