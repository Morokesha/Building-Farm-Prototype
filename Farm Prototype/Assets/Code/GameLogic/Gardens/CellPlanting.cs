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
    
    public class CellPlanting : MonoBehaviour
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

        private void Update()
        {
            ActivatedConstructionMode();
        }

        public void ActivatedFrame(bool activated)
        {
            if (_cellState == CellState.Free) 
                _frame.SetActive(activated);
        }

        private void ActivatedConstructionMode()
        {
            if (_cellState == CellState.Free && _buildingState == BuildingState.WaitBuilt)
                _transparentSurface.SetActive(true);
            else
                _transparentSurface.SetActive(false);
        }

        public void SetCellState(CellState state) => 
            _cellState = state;

        public void SetBuildingState(BuildingState state) => 
            _buildingState = state;

        public CellState GetCellState() => 
            _cellState;
    }
}