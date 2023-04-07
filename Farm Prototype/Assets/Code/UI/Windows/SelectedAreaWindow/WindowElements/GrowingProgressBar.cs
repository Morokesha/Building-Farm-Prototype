using Code.GameLogic.Gardens;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SelectedAreaWindow.WindowElements
{
    public class GrowingProgressBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _processingField;

        private GardenProduction _gardenProduction;

        private readonly float _defaultProgressField = 0f;

        public void UpdateProgressBar(GardenProduction gardenProduction)
        {
            if (_gardenProduction != null)
            {
                if (Equals(_gardenProduction, gardenProduction))
                    return;
                
                _gardenProduction.GrowingChanged -= OnUpdateProgressBar;
                OnUpdateProgressBar(_defaultProgressField);
            }
            _gardenProduction = gardenProduction;
            _gardenProduction.GrowingChanged += OnUpdateProgressBar;
        }
        
        private void OnUpdateProgressBar(float growing) => 
            _processingField.fillAmount = growing;
    }
}