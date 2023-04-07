using Code.Services.ProgressServices;
using Code.UI;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public interface IUIFactory
    {
        public SelectedAreaWindow CreateGardenWindow(UIRoot parentUI);
        
        public ShopWindow CreateShopUI(UIRoot parentUI);
        
        public HUD CreateHud(IProgressDataService progressDataService, ShopWindow shopWindow,
        SelectedAreaWindow selectedAreaWindow,UIRoot parentCanvas);

        public RectTransform CreateNewPanel(RectTransform container);
        public ContentItem CreateContentItem(RectTransform container);
    }
}