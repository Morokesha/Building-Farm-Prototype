using Code.GameLogic.Gardens;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.GardenIfoTab
{
    public class GrowingProgressBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _processingField;

        private GardenProduction _gardenProduction;

        private void Init(GardenProduction gardenProduction)
        {
            _gardenProduction = gardenProduction;
            _gardenProduction.GrowingChanged += OnGrowingChanged;
        }
        
        private void OnGrowingChanged(float growing) => 
            _processingField.fillAmount += growing;
    }
}