using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services
{
    public interface IAssetProvider
    {
        
        GardenAreaVisual GardenAreaVisual { get; }
        Garden Garden { get; }
        GridSell GridSell { get; }
        SelectedGardenWindow SelectedGardenWindow { get; }
        ShopWindow ShopWindow { get; }
        HUD HUD { get; }
        public ContentItem ContentItem { get; }
        public RectTransform ContentPanel { get; }
    }
}