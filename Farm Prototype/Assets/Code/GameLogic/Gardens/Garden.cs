using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Services;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public class Garden : MonoBehaviour
    {
        [SerializeField] 
        private List<RowsProducts> _rowsProducts;

        [SerializeField] 
        private GameObject _visualCell;

        [SerializeField] 
        private GameObject _ground;
      
        private BoxCollider _boxCollider;

        private GardenProduction _gardenProduction;
        private GardenData _gardenData;
        private IResourceService _resourceService;

        public void Init(GardenData gardenData)
        {
            _gardenData = gardenData;
        }
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
        }

        public void ActivateProducts(IResourceService resourceService,
            SeedType type,Vector3 position)
        {
            _resourceService = resourceService;
            _gardenProduction = new GardenProduction(_resourceService);

            transform.position = position;
            _ground.SetActive(true);
            _visualCell.SetActive(false);
            _boxCollider.enabled = true;
            
            foreach (RowsProducts product in _rowsProducts)
            {
                if (product.seedType == type) 
                    product.gameObject.SetActive(true);
            }
        }
    }
}
