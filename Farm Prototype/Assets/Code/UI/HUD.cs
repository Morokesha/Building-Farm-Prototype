using System;
using System.Collections.Generic;
using System.Resources;
using Code.Data.ResourceData;
using Code.Management;
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

        public void Init(IResourceService resourceRepository)
        {
            _resourceRepository = resourceRepository;
            _resourceRepository.ResourcesChanged += ResourceUpdated;
        }

        private void Start()
        {
            _shopMenuBtn.onClick.AddListener(ShowShopMenu);
        }

        private void ShowShopMenu() =>
            _shopUI.ActiveShop(ActiveShopState.Activated);


        private void ResourceUpdated()
        {
            
        }
    }
}