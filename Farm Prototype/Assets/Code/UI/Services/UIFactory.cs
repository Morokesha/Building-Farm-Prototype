using Code.Services;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.Shop;
using Code.UI.Windows.Shop.WindowElements;
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

        public ShopWindow CreateShopUI(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.ShopWindow,
                parentUI.transform.GetComponent<RectTransform>(), false);

        public HUD CreateHud(IProgressDataService progressDataService, ShopWindow shopWindow,
            SelectedGardenWindow selectedGardenWindow, UIRoot parentCanvas)
        {
            HUD hud = Object.Instantiate(_assetProvider.HUD,
                parentCanvas.transform.GetComponent<RectTransform>(), false);
            hud.Init(progressDataService, shopWindow, selectedGardenWindow);

            return hud;
        }

        public RectTransform CreateNewPanel(RectTransform container) => 
            Object.Instantiate(_assetProvider.ContentPanel, container, false);

        public ContentItem CreateContentItem(RectTransform container) =>
            Object.Instantiate(_assetProvider.ContentItem, container, false);
    }
}