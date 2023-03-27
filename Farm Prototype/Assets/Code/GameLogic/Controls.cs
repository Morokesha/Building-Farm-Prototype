using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.GameLogic
{
    public class Controls
    {
        private Camera _camera;

        private GridSell _gridCell;
        private Garden _raycastGarden;

        public void Init() =>
            _camera = Camera.main;

        public GridSell GetGridCell()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out GridSell gridCell))
                {
                    _gridCell = gridCell;
                }
            }

            return _gridCell;
        }

        public Garden GetGarden()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _raycastGarden = hit.collider.TryGetComponent(out Garden garden) ? garden : null;
            }

            return _raycastGarden;
        }

        public Vector3 GetMouseToWorldPosition()
        {
            Vector3 position = Vector3.zero;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out RaycastHit hit)) 
                position = hit.point;

            return position;
        }

        public bool GetMouseClick() => 
            Input.GetMouseButton(0);
    }
}