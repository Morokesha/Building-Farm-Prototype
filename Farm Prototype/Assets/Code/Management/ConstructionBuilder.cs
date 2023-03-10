using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.GameLogic.Gardens;
using Code.Services;
using UnityEngine;

namespace Code.Management
{
    public class ConstructionBuilder : MonoBehaviour
    {
        [SerializeField]
        private Transform _containerForPlanting;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetZ;

        private readonly int _countCellPlanting = 3;
        
        private SeedType _activeSeedType;
        
        private IGameFactory _gameFactory;
        private IShopService _shop;
        
        private Camera _camera;
        private  List<CellPlanting> _listCells;
        private CellPlanting _createdCells;
        private CellPlanting _raycastCell;
        private Garden _activeGardenType;

        private Vector3 _startPosition;
        private Vector3 _createPos;
        private Vector3 _rowOffset;
        private Vector3 _gardenPosition;

        public void Init(IGameFactory gameFactory,IShopService shop)
        {
            _gameFactory = gameFactory;
            _shop = shop;
        }
        
        private void Awake()
        {
            _camera = Camera.main;
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
            if (RaycastCells())
            {
                if (UtilClass.MouseClick())
                {
                    if (_raycastCell != null && _raycastCell.GetCellState() == CellState.Free) 
                        SetGardenOnCell();
                }
            }
        }

        private void SetGardenOnCell()
        {
            if (_activeGardenType)
            {
                _gardenPosition = _raycastCell.GetComponent<Transform>().position;
                _activeGardenType.ActivateProducts(_activeSeedType, _gardenPosition);
                _raycastCell.SetCellState(CellState.Occupied);
                _activeGardenType = null;
            }
        }

        private bool RaycastCells()
        {
            bool rayHitOnCell = false;
            
            Ray ray = _camera.ScreenPointToRay(UtilClass.GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CellPlanting cellPlanting))
                {
                    _raycastCell = cellPlanting;
                    _raycastCell.ActivatedFrame(true);
                    rayHitOnCell = true;
                }
                else
                {
                    if (_raycastCell != null)
                    {
                        _raycastCell.ActivatedFrame(false);
                        _raycastCell = null;
                    }
                }
            }

            return rayHitOnCell;
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
            ActivatedCellConstructionMode(BuildingState.Wait);
        }

        private void ShopOnSoldCells()
        {
            CreateCellForPlanting();
        }
    }
}