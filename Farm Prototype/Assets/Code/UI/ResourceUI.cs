using System;
using System.Collections.Generic;
using System.Resources;
using Code.Data.ResourceData;
using Code.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinTxt;
        [SerializeField]
        private TextMeshProUGUI _seedTxt;

        private ResourceRepository _resourceRepository;

        private ResourceHolder _resourceHolder;
        private Dictionary<ResourceType, Transform> _resourceTypeTransforms;

        private Transform _parentForResourceUI;

        public void Init(ResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
            _resourceRepository.ResourcesChanged += ResourceUpdated;
        }

     
        
        private void ResourceUpdated()
        {
            
        }
    }
}