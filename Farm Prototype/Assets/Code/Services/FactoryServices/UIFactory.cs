using Code.GameLogic.Tutorial;
using Code.Services.AssetServices;
using Code.Services.ProgressServices;
using Code.UI.Windows.HUDWindow;
using Code.UI.Windows.SelectedAreaWindows;
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

        public SelectedAreaWindow CreateGardenWindow(RectTransform container) =>
            _assetProvider.Instantiate<SelectedAreaWindow>(AssetPath.SelectedAreaWindowPath,
                container);

        public ShopWindow CreateShopUI(RectTransform container) =>
            _assetProvider.Instantiate<ShopWindow>(AssetPath.ShopUIPath,
                container);

        public HUD CreateHud(IProgressService progressService,ShopWindow shopWindow, 
            SelectedAreaWindow selectedAreaWindow, UIRoot parentCanvas)
        {
            HUD hud = _assetProvider.Instantiate<HUD>(AssetPath.HudPath,
                parentCanvas.transform.GetComponent<RectTransform>());
            hud.Init(progressService, shopWindow, selectedAreaWindow);

            return hud;
        }

        public RectTransform CreateNewPanel(RectTransform container) =>
            _assetProvider.Instantiate<RectTransform>(AssetPath.ContentPanelPath, container);

        public ContentItem CreateContentItem(RectTransform container) =>
            _assetProvider.Instantiate<ContentItem>(AssetPath.ContentItemPath, container);

        public TutorialWindow CreateTutorialWindow(RectTransform container) =>
            _assetProvider.Instantiate<TutorialWindow>(AssetPath.TutorialWindowPath, container);
    }
}