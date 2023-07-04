using Code.GameLogic.Tutorial;
using Code.Services.ProgressServices;
using Code.UI.Windows.HUDWindow;
using Code.UI.Windows.SelectedAreaWindows;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public interface IUIFactory
    {
        public SelectedAreaWindow CreateGardenWindow(RectTransform container);
        
        public ShopWindow CreateShopUI(RectTransform container);
        
        public HUD CreateHud(IProgressService progressService,ShopWindow shopWindow,
        SelectedAreaWindow selectedAreaWindow,UIRoot parentCanvas);

        public RectTransform CreateNewPanel(RectTransform container);
        public ContentItem CreateContentItem(RectTransform container);
        public TutorialWindow CreateTutorialWindow(RectTransform container);
    }
}