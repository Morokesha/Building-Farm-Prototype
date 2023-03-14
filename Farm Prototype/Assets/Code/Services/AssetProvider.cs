using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI.GardenUI;
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

        public GardenInfoUI GardenInfoUI => _gardenInfoUI;
        
        private Garden _garden;
        private CellPlanting _cellPanting;
        private GardenTypeHolder _gardenTypeHolder;
        private ResourceHolder _resourceHolder;
        private GardenInfoUI _gardenInfoUI;

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
            _gardenInfoUI = Resources.Load<GardenInfoUI>(AssetPath.GardenInfoUIPath);
        }
    }
}