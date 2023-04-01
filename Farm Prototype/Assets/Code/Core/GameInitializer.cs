using Code.GameLogic;
using Code.Management;
using Code.Services;
using Code.UI;
using Code.UI.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
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
        private ShopUI _shopUI;
        private SelectedGardenWindow _selectedGardenWindow;

        public GameInitializer() => 
            RegistrationService();

        public void Init()
        {
            _constructionBuilder = Object.FindObjectOfType<ConstructionBuilder>();
            
            InitUI();

            _shopService.Init(_resourceService, _staticDataService.GardenDataHolder, 
                _staticDataService.CropsDataHolder);
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
            
            _shopUI = _uiFactory.CreateShopUI(uiRoot);
            _selectedGardenWindow = _uiFactory.CreateGardenWindow(uiRoot); 
            _uiFactory.CreateHud(_progressDataService,_shopUI,_selectedGardenWindow,uiRoot);
            _selectedGardenWindow.Init(_constructionBuilder);
            _shopUI.Init(_staticDataService,_shopService,_uiFactory,_constructionBuilder);
        }
    }
}