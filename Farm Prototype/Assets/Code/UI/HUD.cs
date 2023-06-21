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
        public event Action ClickCancel; 

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

        private IProgressDataService _progressDataService;
        private IUpgradeService _upgradeService;

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
            _cancelBtn.onClick.AddListener(OnClickCancel);
        }

        private void Start()
        {
            ActiveCancelBtn(false);

            _upgradeService = _progressDataService.GetUpgradeService;
            _upgradeService.SecondWateringUpgradeActivated += ActiveWateringAllBtn;
            _upgradeService.SecondHarvestingUpgradeActivated += ActiveHarvestingAllBtn;
        }

        public void ActiveCancelBtn(bool active) => 
            _cancelBtn.gameObject.SetActive(active);

        private void ActiveHarvestingAllBtn() => 
            _harvestingAllBtn.gameObject.SetActive(true);

        private void ActiveWateringAllBtn() => 
            _wateringAllBtn.gameObject.SetActive(true);

        private void OnClickCancel()
        {
            ClickCancel?.Invoke();
            ActiveCancelBtn(false);
        }

        private void OnGoldChanged(int amount) => 
            _goldTxt.SetText(amount.ToString());

        private void OnSeedChanged(int amount) => 
            _seedTxt.SetText(amount.ToString());

        private void ShowShopMenu()
        {
            _shopWindow.ActivatedShopMenu();
            _selectedAreaWindow.HideWindow();
        }

        private void OnClickHarvestingAll() => 
            ClickHarvestingAll?.Invoke();

        private void OnClickWateringAll() => 
            ClickWateringAll?.Invoke();
        

        private void OnDestroy()
        {
            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
            _wateringAllBtn.onClick.RemoveListener(OnClickWateringAll);
            _harvestingAllBtn.onClick.RemoveListener(OnClickHarvestingAll);
            
            _progressDataService.GoldChanged -= OnGoldChanged;
            _progressDataService.SeedChanged -= OnSeedChanged;
            
            _upgradeService.SecondWateringUpgradeActivated -= ActiveWateringAllBtn;
            _upgradeService.SecondHarvestingUpgradeActivated -= ActiveHarvestingAllBtn;
        }
    }
}