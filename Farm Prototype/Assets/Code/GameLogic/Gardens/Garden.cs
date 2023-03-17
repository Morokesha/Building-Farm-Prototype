using System;
using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Services;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public class Garden : MonoBehaviour
    {
        public GardenData GetGardenData => _gardenData;
        
        [SerializeField] 
        private List<RowProducts> _rowsProducts;

        [SerializeField] 
        private GameObject _visualCell;

        [SerializeField] 
        private GameObject _ground;

        private IResourceService _resourceService;
        
        private GardenProduction _gardenProduction;
        private GardenData _gardenData;
        private RowProducts _activeProducts;

        private bool _isGrowing;
        
        public void Init(IResourceService resourceService,
            GardenData gardenData)
        {
            _resourceService = resourceService;
            _gardenData = gardenData;
            
            _gardenProduction = new GardenProduction(_resourceService,_gardenData);
            _gardenProduction.GrowingComleted += DeactivatedGrowing;
        }

        private void Update()
        {
            if (_isGrowing)
            {
                _gardenProduction.Growing(_activeProducts);
            }
        }

        public void ActiveProduct(Vector3 position)
        {
            transform.position = position;
            
            _ground.SetActive(true);
            _visualCell.SetActive(false);

            foreach (RowProducts product in _rowsProducts)
            {
                if (product.seedType == _gardenData.SeedType)
                {
                    _activeProducts = product;
                    product.gameObject.SetActive(true); 
                } 
            }
        }

        public GardenProduction GetGardenProduction() => 
            _gardenProduction;
        
        private void DeactivatedGrowing() => 
            _isGrowing = false;

        public void ActiveGrowing() => 
            _isGrowing = true;
    }
}
