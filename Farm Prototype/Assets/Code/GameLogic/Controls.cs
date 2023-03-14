using System;
using Code.GameLogic.Gardens;
using Code.Services;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Code.GameLogic
{
    public class Controls
    {
        private Camera _camera;

        private CellPlanting _raycastCell;
        private Garden _raycastGarden;

        public void Init() =>
            _camera = Camera.main;

        public CellPlanting RaycastCells()
        {
            Ray ray = _camera.ScreenPointToRay(GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CellPlanting cellPlanting))
                {
                    _raycastCell = cellPlanting;
                    _raycastCell.ActivatedFrame(true);
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

            return _raycastCell;
        }

        public Garden RaycastGarden()
        {
            Ray ray = _camera.ScreenPointToRay(GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Garden garden))
                {
                    _raycastGarden = garden;
                }
                else
                {
                    _raycastGarden = null;
                }
            }

            return _raycastGarden;
        }

        public static Vector3 GetMousePosition() => 
            Input.mousePosition;

        public bool GetMouseClick() => 
            Input.GetMouseButton(0);
    }
}