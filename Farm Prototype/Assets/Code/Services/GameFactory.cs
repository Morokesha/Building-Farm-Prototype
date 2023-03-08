using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public class GameFactory : IGameFactory
    { 
        public CellPlanting CreateCellForPlanting(CellPlanting template,Vector3 position,Transform container)
        {
            CellPlanting cell = Object.Instantiate(template, position, Quaternion.identity);
            cell.transform.SetParent(container);
            return cell;
        }

        public Garden CreateGardenBed(Garden template,Vector3 spawnPos)
        {
            Garden garden = Object.Instantiate(template, spawnPos, Quaternion.identity);

            return garden;
        }
    }
}