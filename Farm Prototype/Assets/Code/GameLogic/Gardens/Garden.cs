using Code.Data.GardenData;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.UI.Windows.HUDWindow;
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

        private IProgressService _progressService;
        private IResourceService _resourceService;

        private GardenData _gardenData;
        private GridCell _gridCell;

        public void Init(IProgressService progressService, IResourceService resourceService,
            GardenData gardenData,GridCell gridCell)
        {
            _progressService = progressService;
            _resourceService = resourceService;
            _gardenData = gardenData;
            _gridCell = gridCell;

            _gardenProduction.Init(_resourceService, _gardenData,_gridCell);
            _displayProductionAction.Init(_progressService,_gardenProduction);
        }

        public GardenProduction GetGardenProduction() => 
            _gardenProduction;
    }
}
