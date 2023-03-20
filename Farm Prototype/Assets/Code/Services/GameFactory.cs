using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.GardenIfoTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GridSell CreateCellForPlanting(Vector3 position,Transform container)
        {
            GridSell cell = Object.Instantiate(_assetProvider.GridSell,position, Quaternion.identity);
            cell.transform.SetParent(container);
            return cell;
        }

        public Garden CreateGardenBed(Vector3 spawnPos)
        {
            Garden garden = Object.Instantiate(_assetProvider.Garden, spawnPos, Quaternion.identity);

            return garden;
        }
        public SelectedGardenWindow CreateGardenInfo(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.SelectedGardenWindow, 
                parentUI.transform.GetComponent<RectTransform>(), false);
        
        public ShopUI CreateShopUI(UIRoot parentUI) => 
            Object.Instantiate(_assetProvider.ShopUI, 
                parentUI.transform.GetComponent<RectTransform>(), false);
        
        public HUD CreateHud(IProgressDataService progressDataService, ShopUI shopUI,UIRoot parentCanvas)
        {
            HUD hud = Object.Instantiate(_assetProvider.HUD,
                parentCanvas.transform.GetComponent<RectTransform>(), false);
            hud.Init(progressDataService,shopUI);

            return hud;
        }
    }
}