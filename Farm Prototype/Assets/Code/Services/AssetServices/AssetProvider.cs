using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services.AssetServices
{
    public class AssetProvider : IAssetProvider
    {
        public GardenAreaVisual GardenAreaVisual => _gardenAreaVisual;
        public Garden Garden => _garden;
        public GridSell GridSell => _gridCell;
        public SelectedAreaWindow SelectedAreaWindow => _selectedAreaWindow;
        public ShopWindow ShopWindow => _shopWindow;
        public HUD HUD => _hud;

        public ContentItem ContentItem => _contentItem;
        public RectTransform ContentPanel => _contentPanel;

        private Garden _garden;
        private GridSell _gridCell;
        private SelectedAreaWindow _selectedAreaWindow;
        private ShopWindow _shopWindow;
        private HUD _hud;
        private GardenAreaVisual _gardenAreaVisual;

        private ContentItem _contentItem;
        private RectTransform _contentPanel;
        public AssetProvider()
        {
            LoadAssets();
        }
        
        private void LoadAssets()
        {
            _gardenAreaVisual = Resources.Load<GardenAreaVisual>(AssetPath.GardenAreaVisual);
            _garden = Resources.Load<Garden>(AssetPath.GardenPath);
            _gridCell = Resources.Load<GridSell>(AssetPath.GridCellPath);
            
            _selectedAreaWindow = Resources.Load<SelectedAreaWindow>(AssetPath.SelectedAreaWindowPath);
            _shopWindow = Resources.Load<ShopWindow>(AssetPath.ShopUIPath);
            _hud = Resources.Load<HUD>(AssetPath.HudPath);
            _contentItem = Resources.Load<ContentItem>(AssetPath.ContentItemPath);
            _contentPanel = Resources.Load<RectTransform>(AssetPath.ContentPanelPath);
        }
    }
}