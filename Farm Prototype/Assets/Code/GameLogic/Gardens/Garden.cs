using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Services;
using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public enum BuildingMode
    {
        WaitBuilt,
        Built
    }
    
    public class Garden : MonoBehaviour
    {
        [SerializeField] 
        private List<RowsProducts> _rowsProducts;

        [SerializeField] 
        private GameObject _visualCell;

        [SerializeField] 
        private GameObject _ground;
      
        private BoxCollider _boxCollider;
        
        private BuildingMode _buildingMode;
        private GardenData _gardenData;

        public void Init(GardenData gardenData)
        {
            _gardenData = gardenData;
        }

        public void ActivateProducts(SeedType type,Vector3 position)
        {
            SetBuilt(BuildingMode.Built);
            
            foreach (RowsProducts product in _rowsProducts)
            {
                if (product.seedType == type)
                {
                    product.gameObject.SetActive(true);
                }
            }

            transform.position = position;
            _ground.SetActive(true);
            _visualCell.SetActive(false);
            _boxCollider.enabled = true;
        }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;

            SetBuilt(BuildingMode.WaitBuilt);
        }

        private void Update()
        {
            if (_buildingMode == BuildingMode.WaitBuilt)
            {
                BacklightPosition();
            }
        }

        private void BacklightPosition()
        { 
            Ray ray = Camera.main.ScreenPointToRay(UtilClass.GetMousePosition());
            if (Physics.Raycast(ray,out RaycastHit hit))
            {
                transform.position = hit.point;
            }
            
        }

        private void SetBuilt(BuildingMode mode) => 
            _buildingMode = mode;
    }
}
