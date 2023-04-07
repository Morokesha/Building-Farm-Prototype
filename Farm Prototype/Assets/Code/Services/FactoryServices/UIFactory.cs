using Code.Services.AssetServices;
using Code.Services.ProgressServices;
using Code.UI;
using Code.UI.Windows.SelectedAreaWindow;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public SelectedAreaWindow CreateGardenWindow(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.SelectedAreaWindow,
                parentUI.transform.GetComponent<RectTransform>(), false);

        public ShopWindow CreateShopUI(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.ShopWindow,
                parentUI.transform.GetComponent<RectTransform>(), false);

        public HUD CreateHud(IProgressDataService progressDataService, ShopWindow shopWindow,
            SelectedAreaWindow selectedAreaWindow, UIRoot parentCanvas)
        {
            HUD hud = Object.Instantiate(_assetProvider.HUD,
                parentCanvas.transform.GetComponent<RectTransform>(), false);
            hud.Init(progressDataService, shopWindow, selectedAreaWindow);

            return hud;
        }

        public RectTransform CreateNewPanel(RectTransform container) => 
            Object.Instantiate(_assetProvider.ContentPanel, container, false);

        public ContentItem CreateContentItem(RectTransform container) =>
            Object.Instantiate(_assetProvider.ContentItem, container, false);
    }
}