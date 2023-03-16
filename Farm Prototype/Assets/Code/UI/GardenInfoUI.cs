using System;
using Code.Management;
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
        private Button _removeBtn;
        [SerializeField] 
        private Button BackBtn;

        [SerializeField] 
        private Image _processingField;

        [SerializeField] 
        private CanvasGroup _canvasGroup;

        private ConstructionBuilder _constructionBuilder;
        public void Init(ConstructionBuilder constructionBuilder) => 
            _constructionBuilder = constructionBuilder;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}