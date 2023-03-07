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
            
            CreateResourceTypeTransform();
        }

        private void CreateResourceTypeTransform()
        {
            _resourceHolder = Resources.Load<ResourceHolder>(nameof(ResourceHolder));
            _resourceTypeTransforms = new Dictionary<ResourceType, Transform>();
            
            Transform resourceTemplate = transform.Find("resourceTemplate");
            resourceTemplate.gameObject.SetActive(false);
            int index = 0;
            foreach (ResourceAmount resourceAmount in _resourceHolder.ResourceAmounts)
            {
                Transform resourceTransform = Instantiate(resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);
                float offsetAmount = -160;
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount*index,0);
                resourceTransform.Find("image").GetComponent<Image>().sprite = resourceAmount.ResourceData.sprite;
                _resourceTypeTransforms[resourceAmount.ResourceData.Type] = resourceTransform;
                index++;
            }
        }
        
        private void ResourceUpdated(ResourceType type, int amount)
        {
            foreach (var resourceTypeTransform in _resourceTypeTransforms)
            {
                
            }
        }
    }
}