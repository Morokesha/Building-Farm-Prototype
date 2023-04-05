using Code.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IUIFactory
    {
        public SelectedGardenWindow CreateGardenWindow(UIRoot parentUI);
        
        public ShopWindow CreateShopUI(UIRoot parentUI);
        
        public HUD CreateHud(IProgressDataService progressDataService, ShopWindow shopWindow,
        SelectedGardenWindow selectedGardenWindow,UIRoot parentCanvas);

        public RectTransform CreateNewPanel(RectTransform container);
        public ContentItem CreateContentItem(RectTransform container);
    }
}