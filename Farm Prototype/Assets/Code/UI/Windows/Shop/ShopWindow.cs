using Code.Common;
using Code.Management;
using Code.Services.FactoryServices;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
using Code.Services.UpgradeServices;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private TabHandler _tabHandler;

        [SerializeField] 
        private Button _hideBtn;
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private IStaticDataService _staticData;
        private IShopService _shopService;
        private IUIFactory _uiFactory;
        private IUpgradeService _upgradeService;
        private FarmController _farmController;

        public void Init(IStaticDataService staticData,IShopService shopService,IUIFactory uiFactory, 
            IUpgradeService upgradeService,FarmController farmController)
        {
            _staticData = staticData;
            _shopService = shopService;
            _uiFactory = uiFactory;
            _upgradeService = upgradeService;
            _farmController = farmController;

            _tabHandler.Init(_staticData,_uiFactory, _shopService, _upgradeService);
            
            HideShopMenu();
        }

        private void Start()
        {
            _hideBtn.onClick.AddListener(ExitShopMenu);

            _shopService.ProductPurchased += OnProductPurchased;
        }

        private void OnProductPurchased() => 
            HideShopMenu();

        private void ExitShopMenu()
        {
            _farmController.SetConstructionState(ConstructionState.Select);
            HideShopMenu();
        }

        private void HideShopMenu()
        {
            _canvasGroup.SetActive(false);
        }

        public void ActivatedShopMenu()
        {
            _canvasGroup.SetActive(true);
            _farmController.SetConstructionState(ConstructionState.WaitBuilt);
        }

        private void OnDestroy()
        {
            _hideBtn.onClick.RemoveListener(ExitShopMenu);

            _shopService.ProductPurchased -= OnProductPurchased;
        }
    }
}
