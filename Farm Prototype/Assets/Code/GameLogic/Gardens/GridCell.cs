using System.Collections.Generic;
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
    
    public enum FrameState
    {
        None,
        WaitWatering,
        TakeGold,
        TakeSeed
    }
    
    public class GridCell : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _frame;

        [SerializeField] 
        private GameObject _transparentSurface;

        [SerializeField] 
        private List<MeshRenderer> _frameMaterials;

        private BuildingState _buildingState;
        private CellState _cellState;
        private FrameState _frameState;

        private void Awake()
        {
            SetCellState(CellState.Free);
            SetBuildingState(BuildingState.None);
            DeactivatedFrame();
        }

        public void SetCellState(CellState state) => 
            _cellState = state;

        public void SetBuildingState(BuildingState state) => 
            _buildingState = state;

        public CellState GetGridCellState() => 
            _cellState;

        public void ActiveVisualCell(bool active) => 
            _transparentSurface.SetActive(active);

        public void SetFrameState(FrameState state)
        {
            _frameState = state;
            CheckColorFrame();
        }

        private void ActivatedFrame()
        {
            if (_buildingState == BuildingState.WaitBuilt || 
                _cellState == CellState.Occupied && _buildingState == BuildingState.None) 
                _frame.SetActive(true);
        }

        private void CheckColorFrame()
        {
            switch (_frameState)
            {
                case FrameState.None:
                    SetColor(Color.white);
                    break;
                case FrameState.WaitWatering:
                    SetColor(Color.blue);
                    break;
                case FrameState.TakeGold:
                    SetColor(Color.yellow);
                    break;
                case FrameState.TakeSeed:
                    SetColor(Color.green);
                    break;
            }
        }

        private void SetColor(Color color)
        {
            foreach (MeshRenderer frameMesh in _frameMaterials) 
                frameMesh.material.color = color;
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