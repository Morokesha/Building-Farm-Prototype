using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI
{
    public class ButtonEmission : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] 
        private Outline _outline;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _outline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outline.enabled = false;
        }
    }
}