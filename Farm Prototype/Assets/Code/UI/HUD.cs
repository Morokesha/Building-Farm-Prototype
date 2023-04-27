using System;
using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services.ProgressServices;
using Code.Services.UpgradeServices;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HUD : MonoBehaviour
    {
        public event Action ClickWateringAll; 
        public event Action ClickHarvestingAll; 

        [SerializeField]
        private TextMeshProUGUI _goldTxt;
        [SerializeField]
        private TextMeshProUGUI _seedTxt;
        
        [SerializeField] 
        private Button _shopMenuBtn;

        [SerializeField] 
        private Button _wateringAllBtn;
        [SerializeField]
        private Button _harvestingAllBtn;

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
            
            _shopMenuBtn.onClick.AddListener(ShowShopMenu);
            _wateringAllBtn.onClick.AddListener(OnClickWateringAll);
            _harvestingAllBtn.onClick.AddListener(OnClickHarvestingAll);
        }

        private void OnGoldChanged(int amount) => 
            _goldTxt.SetText(amount.ToString());

        private void OnSeedChanged(int amount) => 
            _seedTxt.SetText(amount.ToString());

        private void Start()
        {
            CheckActiveUpgrades();
        }

        private void ShowShopMenu()
        {
            _shopWindow.ActivatedShopMenu();
            _selectedAreaWindow.HideWindow();
        }

        private void OnClickHarvestingAll() => 
            ClickHarvestingAll?.Invoke();

        private void OnClickWateringAll() => 
            ClickWateringAll?.Invoke();

        private void CheckActiveUpgrades()
        {
            if (_progressDataService.GetUpgradeService.SecondWateringUpgradeActivated)
                _wateringAllBtn.gameObject.SetActive(true);
            if (_progressDataService.GetUpgradeService.SecondHarvestingUpgradeActivated)
                _harvestingAllBtn.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
            _wateringAllBtn.onClick.RemoveListener(OnClickWateringAll);
            _harvestingAllBtn.onClick.RemoveListener(OnClickHarvestingAll);
            
            _progressDataService.GoldChanged -= OnGoldChanged;
            _progressDataService.SeedChanged -= OnSeedChanged;
        }
    }
}