using System;
using Code.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop.WindowElements
{
    public enum NavigationMode
    {
        Back,
        Forward
    }
    
    public class NavigationButtons : MonoBehaviour
    {
        public event Action<NavigationMode> OnClickNavigation;

        [SerializeField] 
        private Button _leftButton;
        [SerializeField] 
        private Button _rightButton;

        private void Start()
        {
            _leftButton.onClick.AddListener(ClickNavigationLeft);
            _rightButton.onClick.AddListener(ClickNavigationRight);
        }

        public void ActiveLeftButton(bool active)
        {
            _leftButton.GetComponent<CanvasGroup>().SetActive(active);
            print("active");
        }

        public void ActiveRightButton(bool active) => 
        _rightButton.GetComponent<CanvasGroup>().SetActive(active);

        private void ClickNavigationLeft() => 
            OnClickNavigation?.Invoke(NavigationMode.Back);

        private void ClickNavigationRight() => 
            OnClickNavigation?.Invoke(NavigationMode.Forward);
    }
}