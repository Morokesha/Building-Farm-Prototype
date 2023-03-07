using System.Collections.Generic;
using Code.Data.GardenBedData;
using Code.Gardens;
using Code.Services;
using UnityEngine;

namespace Code.Management
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] 
        private Shop _shop;
        [SerializeField]
        private CellForPlanting _cellTemplate;
        [SerializeField] 
        private Garden gardenTemplate;

        [SerializeField]
        private Transform _containerForPlanting;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetZ;
        
        private readonly int _countCellPlanting = 3;

        private IGameFactory _gameFactory;
        private SeedType _activeSeedType;

        private Vector3 _startPosition;
        private Vector3 _createPos;
        private Vector3 _rowOffset;
        private Vector3 _gardenPosition;

        private Camera _camera;
        private  List<CellForPlanting> _listCells;
        private CellForPlanting _createdCells;
        private CellForPlanting _raycastCell;
        private Garden _activeGardenType;
        
        private void Awake()
        {
            _gameFactory = new GameFactory();
        }

        private void Start()
        {
            _camera = Camera.main;
            _listCells = new List<CellForPlanting>();
            
            _startPosition = new Vector3(_offsetX,0f,_offsetZ);
            _createPos = _startPosition;
            
            CreateCellForPlanting();
            
            _shop.SoldCells += ShopOnSoldCells;
            _shop.SoldGardenBed += ShopOnSoldGardenBed;
        }
        
        private void Update()
        {
            RaycastCells();

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
            bool cellOnRay = false;
            Ray ray = _camera.ScreenPointToRay(UtilClass.GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.GetComponent<CellForPlanting>())
                {
                    _raycastCell = hit.collider.GetComponent<CellForPlanting>();
                    _raycastCell.ActivatedFrame(true);
                    cellOnRay = true;
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

            return cellOnRay;
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
                    _createdCells = _gameFactory.CreateCellForPlanting(_cellTemplate, _createPos,_containerForPlanting);
                    _createPos += new Vector3(-_offsetX,0f,0f);
                    _listCells.Add(_createdCells);
            }

            _createPos = new Vector3(_startPosition.x, 0f, _createPos.z + _offsetZ);
        }

        private void GardenCreate(SeedType type,Garden garden,Vector3 position)
        {
            _activeGardenType = _gameFactory.CreateGardenBed(garden, position);
            _activeSeedType = type;
        }

        private void ShopOnSoldGardenBed(SeedType seedType)
        {
            GardenCreate(seedType,gardenTemplate,transform.position);
            ActivatedCellConstructionMode(BuildingState.Wait);
        }

        private void ShopOnSoldCells()
        {
            CreateCellForPlanting();
        }
    }
}