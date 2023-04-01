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
        private GardenDataHolder _gardenDataHolder;

        private List<RectTransform> _contentPanelList;
        private List<GardenData> _listGardenData;

        private int _amountItemsOnPanel = 6;

        public void Init(IShopService shopService, GardenDataHolder gardenDataHolder)
        {
            _shopService = shopService;
            _gardenDataHolder = gardenDataHolder;

            _contentPanelList = new List<RectTransform>();
            _listGardenData = new List<GardenData>();
        }

        private void AddContentToPanel()
        {
            _contentPanelList.Add(_contentPanel);
        }
    }
}