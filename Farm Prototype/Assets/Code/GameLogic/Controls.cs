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

        public GridSell GetRaycastGridSell(LayerMask mask)
        {
            GridSell gridSellRaycast = null;
            var hit = Hittable(mask, out var hits);

            if (hit)
                if (hits.collider.TryGetComponent(out GridSell gridSell))
                    gridSellRaycast = gridSell;

            return gridSellRaycast;
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

        public bool GetMouseClick() => 
            Input.GetMouseButton(0);
    }
}