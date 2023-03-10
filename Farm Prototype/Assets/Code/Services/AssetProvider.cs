using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Mono.Cecil;
using UnityEngine;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        public Garden Garden => _garden;
        public CellPlanting CellPlanting => _cellPanting;
        public GardenTypeHolder GardenTypeHolder => _gardenTypeHolder;
        public ResourceHolder ResourceHolder => _resourceHolder;
        
        private Garden _garden;
        private CellPlanting _cellPanting;
        private GardenTypeHolder _gardenTypeHolder;
        private ResourceHolder _resourceHolder;

        public AssetProvider()
        {
            LoadAssets();
        }
        
        private void LoadAssets()
        {
            _garden = Resources.Load<Garden>(AssetPath.GardenPath);
            _cellPanting = Resources.Load<CellPlanting>(AssetPath.CellPlantingPath);
            _gardenTypeHolder = Resources.Load<GardenTypeHolder>(AssetPath.GardenTypeHolderPath);
            _resourceHolder = Resources.Load<ResourceHolder>(AssetPath.ResourceHolderPath);
        }
    }
}