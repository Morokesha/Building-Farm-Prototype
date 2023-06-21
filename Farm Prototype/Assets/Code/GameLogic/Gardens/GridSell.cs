using UnityEngine;

namespace Code.GameLogic.Gardens
{
    public enum CellState
    {
        Occupied,
        Free
    }

    public enum BuildingState
    {
        None,
        WaitBuilt,
    }
    
    public class GridSell : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _frame;

        [SerializeField] 
        private GameObject _transparentSurface;

        private BuildingState _buildingState;
        private CellState _cellState;

        private void Awake()
        {
            SetCellState(CellState.Free);
            SetBuildingState(BuildingState.None);
        }

        public void SetCellState(CellState state) => 
            _cellState = state;

        public void SetBuildingState(BuildingState state) => 
            _buildingState = state;

        public CellState GetGridCellState() => 
            _cellState;

        public void ActiveVisualCell(bool active) => 
            _transparentSurface.SetActive(active);

        private void ActivatedFrame()
        {
            if (_buildingState == BuildingState.WaitBuilt || 
                _cellState == CellState.Occupied && _buildingState == BuildingState.None) 
                _frame.SetActive(true);
        }

        private void DeactivatedFrame()
        {
            _frame.SetActive(false);
        }

        private void OnMouseEnter()
        {
            ActivatedFrame();
        }

        private void OnMouseExit()
        {
            DeactivatedFrame();
        }
    }
}