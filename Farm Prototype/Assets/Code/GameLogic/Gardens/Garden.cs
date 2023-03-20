using System;
using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.GameLogic.Gardens
{
    public enum GardenState
    {
        WaitWatering,
        Growing,
        CompleteGrowth
    }
    
    public class Garden : MonoBehaviour
    {
        public GardenData GetGardenData => _gardenData;
        
        [SerializeField] 
        private List<RowProducts> _rowsProducts;

        [SerializeField] 
        private GameObject _visualCell;

        [SerializeField] 
        private GameObject _gardenVisual;

        private IResourceService _resourceService;
        
        private GardenProduction _gardenProduction;
        private GardenData _gardenData;

        private GardenState _gardenState;

        public void Init(IResourceService resourceService,
            GardenData gardenData)
        {
            _resourceService = resourceService;
            _gardenData = gardenData;
            
            _gardenProduction = new GardenProduction(_resourceService,_gardenData);

            SetGardenState(GardenState.WaitWatering);
        }

        public void ActiveProduct(Vector3 position)
        {
            transform.position = position;

            _gardenVisual.SetActive(true);
            _visualCell.SetActive(false);

            foreach (RowProducts product in _rowsProducts)
            {
                if (product.seedType == _gardenData.SeedType)
                {
                    product.gameObject.SetActive(true);
                    _gardenProduction.SetRowProducts(product);
                }
            }
        }

        public GardenProduction GetGardenProduction() => 
            _gardenProduction;

        public void SetGardenState(GardenState state) => 
            _gardenState = state;

        public GardenState GetGardenState() => 
            _gardenState;
        
    }
}
