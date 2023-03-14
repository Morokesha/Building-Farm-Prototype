using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GardenUI
{
    public class GardenInfoUI : MonoBehaviour
    {
        [SerializeField]
        private Button _wateringBtn;
        [SerializeField]
        private Button _harvestingBtn;
        [SerializeField] 
        private Button BackBtn;

        [SerializeField] 
        private Image _processingField;

        [SerializeField] 
        private Button _removeBtn;
    }
}