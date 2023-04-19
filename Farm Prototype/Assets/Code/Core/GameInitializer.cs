using Code.GameLogic;
using Code.Management;
using Code.Services.AssetServices;
using Code.Services.FactoryServices;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
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
            _constructionBuilder.Init(_gameFactory,_resourceService,_controls,_shopService);
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
        }

        private void InitUI()
        {
            UIRoot uiRoot = Object.FindObjectOfType<UIRoot>();
            
            _shopWindow = _uiFactory.CreateShopUI(uiRoot);
            _selectedAreaWindow = _uiFactory.CreateGardenWindow(uiRoot); 
            _uiFactory.CreateHud(_progressDataService,_shopWindow,_selectedAreaWindow,uiRoot);
            
            _selectedAreaWindow.Init(_constructionBuilder);
            _shopWindow.Init(_staticDataService,_shopService,_uiFactory,_constructionBuilder);
        }
    }
}