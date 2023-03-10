using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        CellPlanting CreateCellForPlanting(Vector3 position,Transform container);
        Garden CreateGardenBed(Vector3 spawnPos);
    }
}