using Code.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IUIFactory
    {
        public SelectedGardenWindow CreateGardenWindow(UIRoot parentUI);
        
        public ShopUI CreateShopUI(UIRoot parentUI);
        
        public HUD CreateHud(IProgressDataService progressDataService, ShopUI shopUI,
        SelectedGardenWindow selectedGardenWindow,UIRoot parentCanvas);

        public RectTransform CreateNewPanel(RectTransform rectTransform, RectTransform container);
        public ContentItem CreateContentItem(ContentItem item, RectTransform container);
    }
}