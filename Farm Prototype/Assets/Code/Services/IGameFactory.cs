using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        GridSell CreateCellForPlanting(Vector3 position,Transform container);
        GardenAreaVisual CreateGardenAreaVisual(Vector3 spawnPos);
        Garden CreateGarden(Vector3 spawnPos);
    }
}