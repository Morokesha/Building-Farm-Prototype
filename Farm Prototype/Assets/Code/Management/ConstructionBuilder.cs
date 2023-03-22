using System;
using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.GameLogic;
using Code.GameLogic.Gardens;
using Code.Services;
using UnityEngine;

namespace Code.Management
{
    public enum BuildingMode
    {
        WaitBuilt,
        Complete
    }
    public class ConstructionBuilder : MonoBehaviour
    {
        public event Action<Garden> SelectedGarden;
        
        [SerializeField]
        private Transform _containerForPlanting;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetZ;

        private readonly int _countCellPlanting = 3;
        
        private BuildingMode _buildingMode;

        private GardenData _activeGardenData;
        
        private IGameFactory _gameFactory;
        private IShopService _shop;
        private IResourceService _resourceService;
        private Controls _controls;

        private  List<GridSell> _listCells;
        private GridSell _createdCells;
        private GridSell _selectedCell;
        private Garden _selectedGarden;
        private Garden _createdGarden;

        private Vector3 _startPosition;
        private Vector3 _createPos;
        private Vector3 _rowOffset;
        private Vector3 _gardenPosition;

        public void Init(IGameFactory gameFactory,IResourceService resourceService,
            Controls controls,IShopService shop)
        {
            _gameFactory = gameFactory;
            _resourceService = resourceService;
            _controls = controls;
            _shop = shop;
        }
        
        private void Awake()
        {
            _buildingMode = BuildingMode.Complete;
            
            _listCells = new List<GridSell>();
            
            _startPosition = new Vector3(_offsetX,0f,_offsetZ);
            _createPos = _startPosition;
            
            CreateCellForPlanting();
        }

        private void Start()
        {
            _shop.SoldGridCells += ShopOnSoldGridCells;
            _shop.SoldGarden += ShopOnSoldGardenBed;
        }

        private void Update()
        {
            UpdateGardenGhostPosition();
            
            if (_controls.GetMouseClick() && _buildingMode == BuildingMode.WaitBuilt) 
                BuiltGarden();
            if (_controls.GetMouseClick() && _buildingMode == BuildingMode.Complete) 
                SelectGarden();
        }

        private void UpdateGardenGhostPosition()
        {
            if (_createdGarden != null) 
                _createdGarden.transform.position = _controls.GetMouseToWorldPosition();
        }

        private void BuiltGarden()
        {
            _selectedCell = _controls.GetGridCell();
            if (_selectedCell != null && _selectedCell.GetGridCellState() == CellState.Free)
                SetGardenOnCell();
        }

        private void SelectGarden()
        {
            _selectedGarden = _controls.GetGarden();
            if (_selectedGarden != null) 
                SelectedGarden?.Invoke(_selectedGarden);
        }

        private void SetGardenOnCell()
        {
            if (_createdGarden)
            {
                _gardenPosition = _selectedCell.GetComponent<Transform>().position;
                _createdGarden.ActiveProduct(_gardenPosition);
                
                _selectedCell.SetCellState(CellState.Occupied);
                _buildingMode = BuildingMode.Complete;
                
                DeactivatedCellConstructionMode(BuildingState.None);
                _createdGarden = null;
            }
        }

        private void CreateCellForPlanting()
        {
            for (int i = 0; i < _countCellPlanting; i++)
            {
                    _createdCells = _gameFactory.CreateCellForPlanting(_createPos,_containerForPlanting);
                    _createPos += new Vector3(-_offsetX,0f,0f);
                    _listCells.Add(_createdCells);
            }
            _createPos = new Vector3(_startPosition.x, 0f, _createPos.z + _offsetZ);
        }

        private void GardenCreate(Vector3 position)
        {
            _createdGarden = _gameFactory.CreateGardenBed(position);
            _createdGarden.Init(_resourceService,_activeGardenData);
            
            _buildingMode = BuildingMode.WaitBuilt;
        }

        private void ShopOnSoldGardenBed(GardenData gardenData)
        {
            _activeGardenData = gardenData;
            GardenCreate(transform.position);
            ActivatedCellConstructionMode(BuildingState.WaitBuilt);
        }
        
        private void ActivatedCellConstructionMode(BuildingState state)
        {
            foreach (var cell in _listCells)
            {
                if (cell.GetGridCellState() == CellState.Free) 
                    cell.SetBuildingState(state);
            }
        }
        private void DeactivatedCellConstructionMode(BuildingState state)
        {
            foreach (var cell in _listCells)
                if (state == BuildingState.None)
                    cell.SetBuildingState(state);
        }

        private void ShopOnSoldGridCells()
        {
            CreateCellForPlanting();
        }

        public void ClearSelectedGarden()
        {
            _selectedGarden = null;
        }
    }
}