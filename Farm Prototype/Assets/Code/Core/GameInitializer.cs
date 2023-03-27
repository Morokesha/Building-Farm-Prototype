using Code.GameLogic;
using Code.Management;
using Code.Services;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Core
{
    public class GameInitializer
    {
        private IProgressDataService _progressDataService;
        private IAssetProvider _assetProvider;
        private IGameFactory _gameFactory;
        private IResourceService _resourceService;
        private IShopService _shopService;
        
        private ConstructionBuilder _constructionBuilder;
        private Controls _controls;
        private UIRoot _uiRoot;
        private HUD _hud;
        private ShopUI _shopUI;
        private SelectedGardenWindow _selectedGardenInfo;

        public GameInitializer() => 
            RegistrationService();

        public void Init()
        {
            _constructionBuilder = Object.FindObjectOfType<ConstructionBuilder>();
            
            InitUI();

            _shopService.Init(_resourceService, _assetProvider.GardenTypeHolder, _shopUI);
            _controls.Init();
            _resourceService.Init(_progressDataService,_assetProvider.ResourceHolder);
            _constructionBuilder.Init(_gameFactory,_resourceService,_controls,_shopService);
        }

        private void RegistrationService()
        {
            _progressDataService = new ProgressDataService();
            _assetProvider = new AssetProvider();
            _gameFactory = new GameFactory(_assetProvider);
            _resourceService = new ResourceRepository();
            _controls = new Controls();
            _shopService = new Shop();
        }

        private void InitUI()
        {
            UIRoot uiRoot = Object.FindObjectOfType<UIRoot>();
            
            _shopUI = _gameFactory.CreateShopUI(uiRoot);
            _hud = _gameFactory.CreateHud(_progressDataService,_shopUI,uiRoot);
            _selectedGardenInfo = _gameFactory.CreateGardenInfo(uiRoot);
            _selectedGardenInfo.Init(_constructionBuilder);
        }
    }
}