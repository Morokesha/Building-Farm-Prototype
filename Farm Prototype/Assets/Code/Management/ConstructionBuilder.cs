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
        [SerializeField]
        private Transform _containerForPlanting;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetZ;

        private readonly int _countCellPlanting = 3;
        
        private SeedType _activeSeedType;
        private BuildingMode _buildingMode;

        private IGameFactory _gameFactory;
        private IShopService _shop;
        private IResourceService _resourceService;
        
        private  List<CellPlanting> _listCells;
        private CellPlanting _createdCells;
        private CellPlanting _raycastCell;
        private Garden _raycastGarden;
        private Garden _activeGardenType;

        private Vector3 _startPosition;
        private Vector3 _createPos;
        private Vector3 _rowOffset;
        private Vector3 _gardenPosition;
        private Controls _controls;

        public void Init(IGameFactory gameFactory,IResourceService resourceService,
            Controls controls,IShopService shop)
        {
            _gameFactory = gameFactory;
            _controls = controls;
            _shop = shop;
        }
        
        private void Awake()
        {
            _listCells = new List<CellPlanting>();
            
            _startPosition = new Vector3(_offsetX,0f,_offsetZ);
            _createPos = _startPosition;
            
            CreateCellForPlanting();
        }

        private void Start()
        {
            _shop.SoldCells += ShopOnSoldCells;
            _shop.SoldGardenBed += ShopOnSoldGardenBed;
        }
        
        private void Update()
        {
            RaycastConstructions();
            BuiltGarden();
        }

        private void RaycastConstructions()
        {
            if (_buildingMode == BuildingMode.WaitBuilt)
                _raycastCell = _controls.RaycastCells();
            else
                _raycastGarden = _controls.RaycastGarden();
        }

        private void BuiltGarden()
        {
            if (_controls.GetMouseClick())
                if (_raycastCell != null && _raycastCell.GetCellState() == CellState.Free)
                    SetGardenOnCell();
        }

        private void SettingGardenPosition()
        {
            if (_activeGardenType != null)
            {
                
            }
        }

        private void SetGardenOnCell()
        {
            if (_activeGardenType)
            {
                _gardenPosition = _raycastCell.GetComponent<Transform>().position;
                _activeGardenType.ActivateProducts(_resourceService,_activeSeedType, _gardenPosition);
                _raycastCell.SetCellState(CellState.Occupied);
                _activeGardenType = null;
            }
        }

        private void ActivatedCellConstructionMode(BuildingState state)
        {
            foreach (var cell in _listCells) 
                cell.SetBuildingState(state);
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

        private void GardenCreate(SeedType type,Vector3 position)
        {
            _activeGardenType = _gameFactory.CreateGardenBed(position);
            _activeSeedType = type;
        }

        private void ShopOnSoldGardenBed(SeedType seedType)
        {
            GardenCreate(seedType,transform.position);
            ActivatedCellConstructionMode(BuildingState.WaitBuilt);
        }

        private void ShopOnSoldCells()
        {
            CreateCellForPlanting();
        }
    }
}