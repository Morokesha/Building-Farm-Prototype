using System;
using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.GameLogic.Gardens
{
    public class Garden : MonoBehaviour
    {
        public GardenData GetGardenData => _gardenData;
        
        [SerializeField] 
        private List<RowProducts> _rowsProducts;
        [SerializeField]
        private GardenProduction _gardenProduction;

        [SerializeField] 
        private GameObject _visualCell;

        [SerializeField] 
        private GameObject _gardenVisual;

        private IResourceService _resourceService;
        
        private GardenData _gardenData;

        public void Init(IResourceService resourceService,
            GardenData gardenData)
        {
            _resourceService = resourceService;
            _gardenData = gardenData;
            
            _gardenProduction.Init(_resourceService,_gardenData);
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

    }
}
