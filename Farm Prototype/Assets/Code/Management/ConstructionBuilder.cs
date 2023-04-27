using System;
using System.Collections.Generic;
using Code.Data.GardenData;
using Code.GameLogic;
using Code.GameLogic.Gardens;
using Code.Services.FactoryServices;
using Code.Services.GardenHandlerService;
using Code.Services.ProgressServices;
using Code.Services.ResourceServices;
using Code.Services.ShopServices;
using Code.Services.UpgradeServices;
using UnityEngine;

namespace Code.Management
{
    public enum ConstructionState
    {
        WaitBuilt,
        Select
    }
    public class ConstructionBuilder : MonoBehaviour
    {
        public event Action<Garden> SelectedGarden;
        
        [SerializeField]
        private Transform _containerForPlanting;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetZ;

        private IProgressDataService _progressDataService;
        private IGameFactory _gameFactory;
        private IShopService _shop;
        private IResourceService _resourceService;
        private IUpgradeService _upgradeService;
        private IGardenHandlerService _gardenHandlerService;

        private ConstructionState _constructionState;

        private  List<GridSell> _listCells;
        private GardenData _activeGardenData;
        private Garden _selectedGarden;
        private GardenAreaVisual _gardenAreaVisual;

        private Controls _controls;

        private GridSell _createdCells;
        private GridSell _selectedCell;

        private Vector3 _startPosition;
        private Vector3 _createPos;
        private Vector3 _rowOffset;
        private Vector3 _gardenPosition;

        private const int NumberOfLoopCalls = 1;
        private const int CountCellPlanting = 3;

        public void Init(IProgressDataService progressDataService,IGameFactory gameFactory,
            IResourceService resourceService,IShopService shop,IGardenHandlerService gardenHandlerService, 
            Controls controls)
        {
            _gameFactory = gameFactory;
            _resourceService = resourceService;
            _controls = controls;
            _shop = shop;
            _progressDataService = progressDataService;
            _gardenHandlerService = gardenHandlerService;
            _upgradeService = _progressDataService.GetUpgradeService;
        }
        
        private void Awake()
        {
            _constructionState = ConstructionState.Select;
            
            _listCells = new List<GridSell>();
            
            _startPosition = new Vector3(_offsetX,0f,_offsetZ);
            _createPos = _startPosition;
            
            CreateCellForPlanting(NumberOfLoopCalls);
        }

        private void Start()
        {
            _shop.SoldGarden += ShopOnSoldGarden;
        }

        private void Update()
        {
            UpdateGardenVisualPosition();
            
            if (_controls.GetMouseClick() && _constructionState == ConstructionState.WaitBuilt) 
                BuiltGarden();
            if (_controls.GetMouseClick() && _constructionState == ConstructionState.Select) 
                SelectGarden();
        }

        public void ClearSelectedGarden() => 
            _selectedGarden = null;

        public void SetConstructionState(ConstructionState state) => 
            _constructionState = state;

        public void ClearGardenAreaVisual()
        {
            SetConstructionState(ConstructionState.Select);
            Destroy(_gardenAreaVisual);
        }

        private void UpdateGardenVisualPosition()
        {
            if (_gardenAreaVisual != null) 
                _gardenAreaVisual.transform.position = _controls.GetMouseToWorldPosition();
        }

        private void BuiltGarden()
        {
            _selectedCell = _controls.GetGridCell();
            if (_gardenAreaVisual != null)
                if (_selectedCell != null && _selectedCell.GetGridCellState() == CellState.Free)
                    CreateGardenOnCell();
        }

        private void SelectGarden()
        {
            _selectedGarden = _controls.GetGarden();
            if (_selectedGarden != null) 
                SelectedGarden?.Invoke(_selectedGarden);
        }

        private void CreateGardenOnCell()
        {
            Garden createdGarden = _gameFactory.CreateGarden(_selectedCell.transform.position);
            createdGarden.Init(_resourceService,_activeGardenData);
            
            _gardenHandlerService.AddGarden(createdGarden);
            
            Destroy(_gardenAreaVisual.gameObject);
            
            _selectedCell.SetCellState(CellState.Occupied);
            
            SetConstructionState(ConstructionState.Select);
            DeactivatedCellConstructionMode(BuildingState.None);
        }

        private void CreateCellForPlanting(int numberOfLoopCalls)
        {
            for (int i = 0; i < numberOfLoopCalls; i++)
            {
                for (int j = 0; j < CountCellPlanting; j++)
                {
                    _createdCells = _gameFactory.CreateCellForPlanting(_createPos,_containerForPlanting);
                    _createPos += new Vector3(-_offsetX,0f,0f);
                    _listCells.Add(_createdCells);
                }
                _createPos = new Vector3(_startPosition.x, 0f, _createPos.z + _offsetZ);
            }
        }

        public void AddGridCells(int numberOfLoopCalls) => 
            CreateCellForPlanting(numberOfLoopCalls);

        private void ShopOnSoldGarden(GardenData gardenData)
        {
            _activeGardenData = gardenData;
            _gardenAreaVisual = _gameFactory.CreateGardenAreaVisual
                (_controls.GetMouseToWorldPosition());
            
            SetConstructionState(ConstructionState.WaitBuilt);
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

        private void OnDestroy()
        {
            _shop.SoldGarden -= ShopOnSoldGarden;
            _gardenHandlerService.ClearAll();
        }
    }
}