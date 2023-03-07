using Code.Gardens;
using UnityEngine;

namespace Code.Services
{
    public interface IGameFactory
    {
        CellForPlanting CreateCellForPlanting(CellForPlanting template,Vector3 position,Transform container);
        Garden CreateGardenBed(Garden template, Vector3 spawnPos);
    }
}