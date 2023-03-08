using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        CellPlanting CreateCellForPlanting(CellPlanting template,Vector3 position,Transform container);
        Garden CreateGardenBed(Garden template, Vector3 spawnPos);
    }
}