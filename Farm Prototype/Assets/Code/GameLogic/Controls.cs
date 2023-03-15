using Code.GameLogic.Gardens;
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
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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

        public Vector3 GetMouseToWorldPosition()
        {
            Vector3 position = Vector3.zero;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out RaycastHit hit))
            {
                position = hit.point;
            }

            return position;
        }

        public bool GetMouseClick() => 
            Input.GetMouseButton(0);
    }
}