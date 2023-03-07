using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GardenUI
{
    public class HarvestingUI : MonoBehaviour
    {
        [SerializeField] 
        private Button _harvestingBtn;
        [SerializeField]
        private Button _wateringBtn;

        private Canvas _canvas;
        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
            
            _wateringBtn.onClick.AddListener(ActivateWatering);
        }

        private void ActivateWatering()
        {
            print("WATERING");
        }
    }
}