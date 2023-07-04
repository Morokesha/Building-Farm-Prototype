using Code.GameLogic.Gardens;
using UnityEngine;

namespace Code.GameLogic
{
    public class Controls
    {
        private Camera _camera;

        private GridCell _gridCell;
        private Garden _raycastGarden;

        public void Init() => 
            _camera = Camera.main;

        public GridCell GetRaycastGridSell(LayerMask mask)
        {
            GridCell gridCellRaycast = null;
            var hit = Hittable(mask, out var hits);

            if (hit)
                if (hits.collider.TryGetComponent(out GridCell gridSell))
                    gridCellRaycast = gridSell;

            return gridCellRaycast;
        }

        public Garden GetRaycastGarden(LayerMask mask)
        {
            Garden gardenRaycast = null;
            var hit = Hittable(mask, out var hits);

            if (hit)
                if (hits.collider.TryGetComponent(out Garden garden))
                    gardenRaycast = garden;

            return gardenRaycast;
        }
        
        private bool Hittable(LayerMask mask, out RaycastHit hits)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray.origin, ray.direction, out hits, 100f, mask);
            return hit;
        }

        public Vector3 GetMouseToWorldPosition()
        {
            Vector3 position = Vector3.zero;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out RaycastHit hit)) 
                position = hit.point;

            return position;
        }
    }
}