using Code.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public SelectedGardenWindow CreateGardenWindow(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.SelectedGardenWindow,
                parentUI.transform.GetComponent<RectTransform>(), false);

        public ShopUI CreateShopUI(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.ShopUI,
                parentUI.transform.GetComponent<RectTransform>(), false);

        public HUD CreateHud(IProgressDataService progressDataService, ShopUI shopUI,
            SelectedGardenWindow selectedGardenWindow, UIRoot parentCanvas)
        {
            HUD hud = Object.Instantiate(_assetProvider.HUD,
                parentCanvas.transform.GetComponent<RectTransform>(), false);
            hud.Init(progressDataService, shopUI, selectedGardenWindow);

            return hud;
        }

        public RectTransform CreateNewPanel(RectTransform rectTransform,RectTransform container) => 
            Object.Instantiate(rectTransform, container, false);

        public ContentItem CreateContentItem(ContentItem item, RectTransform container) =>
            Object.Instantiate(item, container, false);
    }
}