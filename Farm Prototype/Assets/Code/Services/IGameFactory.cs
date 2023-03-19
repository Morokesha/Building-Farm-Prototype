using Code.GameLogic.Gardens;
using Code.UI;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        GridSell CreateCellForPlanting(Vector3 position,Transform container);
        Garden CreateGardenBed(Vector3 spawnPos);
        GardenInfoUI CreateGardenInfo(UIRoot parentUI);
        ShopUI CreateShopUI(UIRoot parentUI);
        HUD CreateHud(IResourceService resourceService, ShopUI shopUI,UIRoot parentCanvas);
    }
}