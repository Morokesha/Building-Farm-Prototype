﻿using Code.GameLogic.Gardens;
using Code.Services.AssetServices;
using UnityEngine;

namespace Code.Services.FactoryServices
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GridSell CreateCellForPlanting(Vector3 position,Transform container)
        {
            GridSell cell = Object.Instantiate(_assetProvider.GridSell,position, Quaternion.identity);
            cell.transform.SetParent(container);
            return cell;
        }

        public GardenAreaVisual CreateGardenAreaVisual(Vector3 spawnPos) => 
            Object.Instantiate(_assetProvider.GardenAreaVisual, spawnPos, Quaternion.identity);

        public Garden CreateGarden(Vector3 spawnPos)
        {
            Garden garden = Object.Instantiate(_assetProvider.Garden, spawnPos, Quaternion.identity);

            return garden;
        }
    }
}