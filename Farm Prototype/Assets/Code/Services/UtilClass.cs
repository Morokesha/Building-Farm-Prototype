using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Services
{
    public static class UtilClass
    {
        public static Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public static Vector3 GetMouseToWorldPosition(Camera worldCamera)
        {
            Vector3 worldPos = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            return worldPos;
        }

        public static bool MouseClick()
        {
            return Input.GetMouseButton(0);
        }
    }
}