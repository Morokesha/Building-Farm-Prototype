using System;
using System.Collections.Generic;
using Code.Data.GardenData;
using Code.Services;
using UnityEngine;

namespace Code.UI.Windows.ShopTab
{
    public class ShopCropsContent : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _contentPanel;

        [SerializeField]
        private ContentItem _contentItem;
        
        private IShopService _shopService;
        private GardenTypeHolder _gardenTypeHolder;

        private List<RectTransform> _contentPanelList;
        private List<GardenData> _listGardenData;

        private int _amountItemsOnPanel = 6;

        public void Init(IShopService shopService, GardenTypeHolder gardenTypeHolder)
        {
            _shopService = shopService;
            _gardenTypeHolder = gardenTypeHolder;

            _contentPanelList = new List<RectTransform>();
            _listGardenData = new List<GardenData>();
        }

        private void AddContentToPanel()
        {
            _contentPanelList.Add(_contentPanel);
        }
    }
}