using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.GardenIfoTab;
using Code.UI.Windows.ShopTab;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        GridSell CreateCellForPlanting(Vector3 position,Transform container);
        Garden CreateGardenBed(Vector3 spawnPos);
        SelectedGardenWindow CreateGardenInfo(UIRoot parentUI);
        ShopUI CreateShopUI(UIRoot parentUI);
        HUD CreateHud(IProgressDataService progressDataService, ShopUI shopUI,UIRoot parentCanvas);
    }
}