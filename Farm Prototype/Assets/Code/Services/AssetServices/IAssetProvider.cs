using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services.AssetServices
{
    public interface IAssetProvider
    {
        
        GardenAreaVisual GardenAreaVisual { get; }
        Garden Garden { get; }
        GridCell GridCell { get; }
        SelectedAreaWindow SelectedAreaWindow { get; }
        ShopWindow ShopWindow { get; }
        HUD HUD { get; }
        public ContentItem ContentItem { get; }
        public RectTransform ContentPanel { get; }
    }
}