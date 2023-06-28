using Code.GameLogic;
using Code.GameLogic.Gardens;
using Code.Management;
using Code.Services.AssetServices;
using Code.Services.FactoryServices;
using Code.Services.GardenHandlerService;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
using Code.Services.UpgradeServices;
using Code.UI;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using UnityEngine;

namespace Code.Core
{
    public class GameInitializer
    {
        private IProgressDataService _progressDataService;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;
        private IUIFactory _uiFactory;
        private IGameFactory _gameFactory;
        private IResourceService _resourceService;
        private IShopService _shopService;
        private IUpgradeService _upgradeService;
        private IGardenHandlerService _gardenHandlerService;

        private ConstructionBuilder _constructionBuilder;
        private Controls _controls;
        private UIRoot _uiRoot;
        private HUD _hud;
        private ShopWindow _shopWindow;
        private SelectedAreaWindow _selectedAreaWindow;

        public GameInitializer() => 
            RegistrationService();

        public void Init()
        {
            _constructionBuilder = Object.FindObjectOfType<ConstructionBuilder>();
            
            InitUI();
            
            _shopService.Init(_resourceService);
            _controls.Init();
            _resourceService.Init(_progressDataService, _staticDataService.ResourceHolder);
            _upgradeService.Init(_shopService, _constructionBuilder);
            _progressDataService.Init(_upgradeService);
            _gardenHandlerService.Init(_hud);
            _constructionBuilder.Init(_progressDataService,_gameFactory,_resourceService,_shopService,
                _gardenHandlerService,_hud,_controls);
        }

        private void RegistrationService()
        {
            _progressDataService = new ProgressDataService();
            _staticDataService = new StaticDataService();
            _assetProvider = new AssetProvider();
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
            
            _shopWindow = _uiFactory.CreateShopUI(uiRoot);
            _selectedAreaWindow = _uiFactory.CreateGardenWindow(uiRoot); 
            _selectedAreaWindow.Init(_gardenHandlerService,_progressDataService,_constructionBuilder);
            _hud = _uiFactory.CreateHud(_progressDataService,_shopWindow,_selectedAreaWindow,uiRoot);
            
            _shopWindow.Init(_staticDataService,_shopService,_uiFactory,_upgradeService,_constructionBuilder);
        }
    }
}