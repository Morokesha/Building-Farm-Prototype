using Code.GameLogic.Gardens;
using Code.Services.AssetServices;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GridCell CreateCellForPlanting(Vector3 position,Transform container)
        {
            GridCell cell = _assetProvider.Instantiate<GridCell>(AssetPath.GridCellPath,position,container);
            return cell;
        }

        public GardenAreaVisual CreateGardenAreaVisual(Vector3 spawnPos) =>
            _assetProvider.Instantiate<GardenAreaVisual>(AssetPath.GardenAreaVisual,spawnPos);

        public Garden CreateGarden(Vector3 spawnPos)
        {
            Garden garden = _assetProvider.Instantiate<Garden>(AssetPath.GardenPath,spawnPos);
            return garden;
        }
    }
}