using Code.Gardens;
using Code.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Management
{
    public class GameFactory : IGameFactory
    { 
        public CellForPlanting CreateCellForPlanting(CellForPlanting template,Vector3 position,Transform container)
        {
            CellForPlanting cell = Object.Instantiate(template, position, Quaternion.identity);
            cell.transform.SetParent(container);
            return cell;
        }

        public Garden CreateGardenBed(Gardens.Garden template,Vector3 spawnPos)
        {
            Garden garden = Object.Instantiate(template, spawnPos, Quaternion.identity);

            return garden;
        }
    }
}