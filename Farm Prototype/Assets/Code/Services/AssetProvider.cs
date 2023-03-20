using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.GardenIfoTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        public Garden Garden => _garden;
        public GridSell GridSell => _cellPanting;
        public GardenTypeHolder GardenTypeHolder => _gardenTypeHolder;
        public ResourceHolder ResourceHolder => _resourceHolder;

        public SelectedGardenWindow SelectedGardenWindow => _selectedGardenWindow;
        public ShopUI ShopUI => _shopUI;
        public HUD HUD => _hud;

        private Garden _garden;
        private GridSell _cellPanting;
        private GardenTypeHolder _gardenTypeHolder;
        private ResourceHolder _resourceHolder;
        private SelectedGardenWindow _selectedGardenWindow;
        private ShopUI _shopUI;
        private HUD _hud;

        public AssetProvider()
        {
            LoadAssets();
        }
        
        private void LoadAssets()
        {
            _garden = Resources.Load<Garden>(AssetPath.GardenPath);
            _cellPanting = Resources.Load<GridSell>(AssetPath.CellPlantingPath);
            _gardenTypeHolder = Resources.Load<GardenTypeHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
            _selectedGardenWindow = Resources.Load<SelectedGardenWindow>(AssetPath.GardenInfoUIPath);
            _shopUI = Resources.Load<ShopUI>(AssetPath.ShopUIPath);
            _hud = Resources.Load<HUD>(AssetPath.HudPath);
        }
    }
}