using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public interface IGameFactory
    {
        GridCell CreateCellForPlanting(Vector3 position,Transform container);
        GardenAreaVisual CreateGardenAreaVisual(Vector3 spawnPos);
        Garden CreateGarden(Vector3 spawnPos);
    }
}