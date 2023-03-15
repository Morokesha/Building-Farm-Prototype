using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.GardenUI;
using Mono.Cecil;
using UnityEngine;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        public Garden Garden => _garden;
        public CellPlanting CellPlanting => _cellPanting;
        public GardenTypeHolder GardenTypeHolder => _gardenTypeHolder;
        public ResourceHolder ResourceHolder => _resourceHolder;

        public GardenInfoUI GardenInfoUI => _gardenInfoUI;
        public ShopUI ShopUI => _shopUI;
        public HUD HUD => _hud;
        public UIRoot UIRoot => _uiRoot;
        
        private Garden _garden;
        private CellPlanting _cellPanting;
        private GardenTypeHolder _gardenTypeHolder;
        private ResourceHolder _resourceHolder;
        private GardenInfoUI _gardenInfoUI;
        private ShopUI _shopUI;
        private HUD _hud;
        private UIRoot _uiRoot;

        public AssetProvider()
        {
            LoadAssets();
        }
        
        private void LoadAssets()
        {
            _garden = Resources.Load<Garden>(AssetPath.GardenPath);
            _cellPanting = Resources.Load<CellPlanting>(AssetPath.CellPlantingPath);
            _gardenTypeHolder = Resources.Load<GardenTypeHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
            _gardenInfoUI = Resources.Load<GardenInfoUI>(AssetPath.GardenInfoUIPath);
            _shopUI = Resources.Load<ShopUI>(AssetPath.ShopUIPath);
            _hud = Resources.Load<HUD>(AssetPath.HudPath);
            _uiRoot = Resources.Load<UIRoot>(AssetPath.UIRootPath);
        }
    }
}