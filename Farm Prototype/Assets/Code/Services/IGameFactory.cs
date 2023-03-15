using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.GardenUI;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        CellPlanting CreateCellForPlanting(Vector3 position,Transform container);
        Garden CreateGardenBed(Vector3 spawnPos);
        GardenInfoUI CreateGardenInfo();
        ShopUI CreateShopUI();
        HUD CreateHud();
        UIRoot CreateUIRoot();
    }
}