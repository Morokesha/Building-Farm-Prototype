using Code.GameLogic.Gardens;
using Code.Management;
using Code.UI;
using Code.UI.GardenUI;
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
        public GardenInfoUI CreateGardenInfo(UIRoot parentUI) =>
            Object.Instantiate(_assetProvider.GardenInfoUI, 
                parentUI.transform.GetComponent<RectTransform>(), false);
        
        public ShopUI CreateShopUI(UIRoot parentUI) => 
            Object.Instantiate(_assetProvider.ShopUI, 
                parentUI.transform.GetComponent<RectTransform>(), false);
        
        public HUD CreateHud(IResourceService resourceService, ShopUI shopUI,UIRoot parentCanvas)
        {
            HUD hud = Object.Instantiate(_assetProvider.HUD,
                parentCanvas.transform.GetComponent<RectTransform>(), false);
            hud.Init(resourceService,shopUI);

            return hud;
        }
    }
}