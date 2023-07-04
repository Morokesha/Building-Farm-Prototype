using System;
using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.GameLogic.Tutorial;
using Code.Services.ProgressServices;
using Code.Services.UpgradeServices;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using Code.UI.Windows.SelectedAreaWindows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.HUDWindow
{
    public class HUD : MonoBehaviour
    {
        public event Action ShopOpened;
        public event Action ClickWateringAll; 
        public event Action ClickHarvestingAll;
        public event Action ClickCancel;

        public SelectedAreaWindow SelectedAreaWindow => _selectedAreaWindow;

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
        [SerializeField] 
        private Button _cancelBtn;
        
        private ShopWindow _shopWindow;
        private SelectedAreaWindow _selectedAreaWindow;

        private IProgressService _progressService;
        private IUpgradeService _upgradeService;

        private ResourceHolder _resourceHolder;

        private Dictionary<ResourceType, Transform> _resourceTypeTransforms;

        private Transform _parentForResourceUI;

        public void Init(IProgressService progressService, ShopWindow shopWindow, SelectedAreaWindow selectedAreaWindow)
        {
            _shopWindow = shopWindow;
            _selectedAreaWindow = selectedAreaWindow;
            _progressService = progressService;

            _progressService.GoldChanged += OnGoldChanged;
            _progressService.SeedChanged += OnSeedChanged;

            _shopMenuBtn.onClick.AddListener(ShowShopMenu);
            _wateringAllBtn.onClick.AddListener(OnClickWateringAll);
            _harvestingAllBtn.onClick.AddListener(OnClickHarvestingAll);
            _cancelBtn.onClick.AddListener(OnClickCancel);
        }

        private void Start()
        {
            ActiveCancelBtn(false);

            _upgradeService = _progressService.GetUpgradeService;
            _upgradeService.SecondWateringUpgradeActivated += ActiveWateringAllBtn;
            _upgradeService.SecondHarvestingUpgradeActivated += ActiveHarvestingAllBtn;
        }

        public void ActiveShopBtn(bool active) => 
            _shopMenuBtn.gameObject.SetActive(active);

        public void ActiveCancelBtn(bool active) => 
            _cancelBtn.gameObject.SetActive(active);

        private void ActiveHarvestingAllBtn(ContentItem contentItem) => 
            _harvestingAllBtn.gameObject.SetActive(true);

        private void ActiveWateringAllBtn(ContentItem contentItem) => 
            _wateringAllBtn.gameObject.SetActive(true);

        private void OnClickCancel()
        {
            ClickCancel?.Invoke();
            ActiveCancelBtn(false);
            ActiveShopBtn(true);
        }

        private void OnGoldChanged(int amount) => 
            _goldTxt.SetText(amount.ToString());

        private void OnSeedChanged(int amount) => 
            _seedTxt.SetText(amount.ToString());

        private void ShowShopMenu()
        {
            _shopWindow.ActivatedShopMenu();
            _selectedAreaWindow.HideWindow();
            ShopOpened?.Invoke();
        }

        private void OnClickHarvestingAll() => 
            ClickHarvestingAll?.Invoke();

        private void OnClickWateringAll() => 
            ClickWateringAll?.Invoke();
        

        private void OnDestroy()
        {
            _progressService.GoldChanged -= OnGoldChanged;
            _progressService.SeedChanged -= OnSeedChanged;
            _upgradeService.SecondWateringUpgradeActivated -= ActiveWateringAllBtn;
            _upgradeService.SecondHarvestingUpgradeActivated -= ActiveHarvestingAllBtn;

            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
            _wateringAllBtn.onClick.RemoveListener(OnClickWateringAll);
            _harvestingAllBtn.onClick.RemoveListener(OnClickHarvestingAll);
            _cancelBtn.onClick.RemoveListener(OnClickCancel);
        }
    }
}