using Code.GameLogic;
using Code.Management;
using Code.Services;
using Code.UI;
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
        private ShopUI _shopUI;
        private Controls _controls;

        public void Init()
        {
            RegistrationService();
            
            _controls.Init();
            _resourceService.Init(_progressDataService,_assetProvider.ResourceHolder);

            _constructionBuilder = Object.FindObjectOfType<ConstructionBuilder>();
            _constructionBuilder.Init(_gameFactory,_resourceService,_controls,_shopService);
        }

        private void RegistrationService()
        {
            _progressDataService = new ProgressDataService();
            _assetProvider = new AssetProvider();
            _gameFactory = new GameFactory(_assetProvider);
            _resourceService = new ResourceRepository();
            _controls = new Controls();
            
            _shopUI = Object.FindObjectOfType<ShopUI>();
            _shopService = new Shop(_resourceService,_assetProvider.GardenTypeHolder,_shopUI);
        }
    }
}