using Code.GameLogic;
using Code.GameLogic.Gardens;
using Code.GameLogic.Tutorial;
using Code.Management;
using Code.Services.AssetServices;
using Code.Services.FactoryServices;
using Code.Services.GardenHandlerService;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
using Code.Services.UpgradeServices;
using Code.UI.Windows.HUDWindow;
using Code.UI.Windows.SelectedAreaWindows;
using Code.UI.Windows.Shop;
using UnityEngine;

namespace Code.Core
{
    public class GameInitializer
    {
        private IProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;
        private IUIFactory _uiFactory;
        private IGameFactory _gameFactory;
        private IResourceService _resourceService;
        private IShopService _shopService;
        private IUpgradeService _upgradeService;
        private IGardenHandlerService _gardenHandlerService;

        private FarmController _farmController;
        private TutorialController _tutorialController;
        private Controls _controls;
        private HUD _hud;
        private ShopWindow _shopWindow;
        private SelectedAreaWindow _selectedAreaWindow;
        private TutorialWindow _tutorialWindow;

        public GameInitializer() => 
            RegistrationService();

        public void Init()
        {
            _farmController = Object.FindObjectOfType<FarmController>();
            
            InitUI();
            
            _shopService.Init(_resourceService,_hud);
            _controls.Init();
            _resourceService.Init(_progressService, _staticDataService.ResourceHolder);
            _upgradeService.Init(_shopService, _farmController);
            _progressService.Init(_upgradeService);
            _gardenHandlerService.Init(_hud);
            _farmController.Init(_progressService,_gameFactory,_resourceService,_shopService,
                _gardenHandlerService,_hud,_controls);
            
            InitTutorial();
        }

        private void RegistrationService()
        {
            _assetProvider = new AssetProvider();
            _progressService = new ProgressService();
            _staticDataService = new StaticDataService(_assetProvider);
            _uiFactory = new UIFactory(_assetProvider);
            _gameFactory = new GameFactory(_assetProvider);
            _resourceService = new ResourceRepository();
            _controls = new Controls();
            _shopService = new Shop();
            _upgradeService = new UpgradeHandler();
            _gardenHandlerService = new GardenHandler();
        }

        private void InitUI()
        {
            UIRoot uiRoot = Object.FindObjectOfType<UIRoot>();
            RectTransform uiRootRect = uiRoot.GetComponent<RectTransform>();
            _shopWindow = _uiFactory.CreateShopUI(uiRootRect);
            _selectedAreaWindow = _uiFactory.CreateGardenWindow(uiRootRect); 
            _selectedAreaWindow.Init(_gardenHandlerService,_progressService,_farmController);
            _hud = _uiFactory.CreateHud(_progressService, _shopWindow, 
                _selectedAreaWindow,uiRoot);
            
            _shopWindow.Init(_staticDataService,_shopService,_uiFactory,_upgradeService,_farmController);
            _tutorialWindow = _uiFactory.CreateTutorialWindow(uiRootRect);
        }
        
        private void InitTutorial()
        {
            _tutorialController = new TutorialController(_progressService, _assetProvider, _shopService,
                _farmController, _hud);
            
            _tutorialWindow.Init(_tutorialController);
            _tutorialController.Init();
        }
    }
}