using Code.Data.GardenData;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.Services.UpgradeServices;
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

        private IProgressDataService _progressService;
        private IResourceService _resourceService;

        private GardenData _gardenData;

        public void Init(IProgressDataService progressService, IResourceService resourceService,
            GardenData gardenData)
        {
            _progressService = progressService;
            _resourceService = resourceService;
            _gardenData = gardenData;

            _gardenProduction.Init(_resourceService, _gardenData);
            _displayProductionAction.Init(_progressService,_gardenProduction);
        }

        public GardenProduction GetGardenProduction() => 
            _gardenProduction;
    }
}
