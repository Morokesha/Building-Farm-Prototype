using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        public GardenAreaVisual GardenAreaVisual => _gardenAreaVisual;
        public Garden Garden => _garden;
        public GridSell GridSell => _cellPanting;
        public GardenTypeHolder GardenTypeHolder => _gardenTypeHolder;
        public ResourceHolder ResourceHolder => _resourceHolder;

        public SelectedGardenWindow SelectedGardenWindow => _selectedAreaWindow;
        public ShopUI ShopUI => _shopUI;
        public HUD HUD => _hud;

        private Garden _garden;
        private GridSell _cellPanting;
        private GardenTypeHolder _gardenTypeHolder;
        private ResourceHolder _resourceHolder;
        private SelectedGardenWindow _selectedAreaWindow;
        private ShopUI _shopUI;
        private HUD _hud;
        private GardenAreaVisual _gardenAreaVisual;

        public AssetProvider()
        {
            LoadAssets();
        }
        
        private void LoadAssets()
        {
            _gardenAreaVisual = Resources.Load<GardenAreaVisual>(AssetPath.GardenAreaVisual);
            _garden = Resources.Load<Garden>(AssetPath.GardenPath);
            _cellPanting = Resources.Load<GridSell>(AssetPath.CellPlantingPath);
            _gardenTypeHolder = Resources.Load<GardenTypeHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
            _selectedAreaWindow = Resources.Load<SelectedGardenWindow>(AssetPath.SelectedAreaWindowPath);
            _shopUI = Resources.Load<ShopUI>(AssetPath.ShopUIPath);
            _hud = Resources.Load<HUD>(AssetPath.HudPath);
        }
    }
}