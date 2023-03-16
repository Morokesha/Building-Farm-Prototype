using System;
using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinTxt;
        [SerializeField]
        private TextMeshProUGUI _seedTxt;
        
        [SerializeField] 
        private Button _shopMenuBtn;

        private ShopUI _shopUI;

        private IResourceService _resourceRepository;

        private ResourceHolder _resourceHolder;

        private Dictionary<ResourceType, Transform> _resourceTypeTransforms;

        private Transform _parentForResourceUI;

        public void Init(IResourceService resourceRepository,ShopUI shopUI)
        {
            _shopUI = shopUI;
            _resourceRepository = resourceRepository;
            _resourceRepository.ResourcesChanged += ResourceUpdated;
        }

        private void OnEnable()=>
        _shopMenuBtn.onClick.AddListener(ShowShopMenu);

        private void ShowShopMenu() =>
            _shopUI.ActivatedShopMenu();


        private void ResourceUpdated()
        {
            
        }

        private void OnDisable()
        {
            _shopMenuBtn.onClick.RemoveListener(ShowShopMenu);
        }
    }
}