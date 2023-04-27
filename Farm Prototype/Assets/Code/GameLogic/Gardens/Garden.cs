using Code.Data.GardenData;
using Code.Services.ResourceServices;
using Code.UI;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public class Garden : MonoBehaviour
    {
        public GardenData GetGardenData => _gardenData;
        
        [SerializeField]
        private GardenProduction _gardenProduction;
        [SerializeField] 
        private DisplayProductionAction _displayProductionAction;

        private IResourceService _resourceService;
        
        private GardenData _gardenData;

        public void Init(IResourceService resourceService,
            GardenData gardenData)
        {
            _resourceService = resourceService;
            _gardenData = gardenData;
            
            _gardenProduction.Init(_resourceService,_gardenData);
        }

        public GardenProduction GetGardenProduction() => 
            _gardenProduction;
    }
}
