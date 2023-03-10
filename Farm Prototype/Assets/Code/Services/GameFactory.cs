using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public CellPlanting CreateCellForPlanting(Vector3 position,Transform container)
        {
            CellPlanting cell = Object.Instantiate(_assetProvider.CellPlanting,position, Quaternion.identity);
            cell.transform.SetParent(container);
            return cell;
        }

        public Garden CreateGardenBed(Vector3 spawnPos)
        {
            Garden garden = Object.Instantiate(_assetProvider.Garden, spawnPos, Quaternion.identity);

            return garden;
        }
    }
}