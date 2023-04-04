using System.Collections.Generic;
using System.Linq;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Services;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.UI.Windows.Shop
{
    public class TabSection : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _containerPanels;
        [SerializeField] 
        private RectTransform _panelTemplate;

        [SerializeField]
        private ContentItem _contentItem;

        private IUIFactory _uiFactory;
        private IShopService _shopService;
        private IStaticDataService _staticDataService;
        
        private List<GardenData> _listGardenData;

        private List<ShopItemData> _shopItemDataList;
        private List<RectTransform> _cropsContentList;
        private List<RectTransform> _upgradeContentList;

        private ShopItemType _shopItemType;

        private readonly int _amountItemsOnPanel = 6;

        public void Init(IStaticDataService staticDataService,IUIFactory uiFactory,IShopService shopService)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _shopService = shopService;

            _shopItemType = ShopItemType.Crops;
            
            _listGardenData = new List<GardenData>();
            
            _shopItemDataList = new List<ShopItemData>();
            _cropsContentList = new List<RectTransform>();
            _upgradeContentList = new List<RectTransform>();
            
            CreateSortListShopData();
            AddContentPanel();
            CreateContentItem();
        }

        private void CreateSortListShopData()
        {
            if (_shopItemType == ShopItemType.Crops)
            {
                _shopItemDataList = _staticDataService.ShopItemDataHolder.ShopItemDataList.
                    OrderBy(x => x.PriceData.GoldAmount).ToList();
                
            }

            if (_shopItemType == ShopItemType.Upgrade)
            {
                
            }
        }

        private void AddContentPanel()
        {
            for (int i = 0; i < _shopItemDataList.Count; i++)
            {
                if (i % _amountItemsOnPanel == 0)
                {
                    RectTransform panelWithContent = _uiFactory.CreateNewPanel
                        (_panelTemplate, _containerPanels);
                    _cropsContentList.Add(panelWithContent);
                }
            }
            
            _panelTemplate.gameObject.SetActive(false);
        }

        private void CreateContentItem()
        {
            int lustIndex = 0;
            for (int i = 0; i < _shopItemDataList.Count; i++)
            {
                if (i > 0 && i % _amountItemsOnPanel == 0) 
                    lustIndex++;
                
                ContentItem item = _uiFactory.CreateContentItem(_contentItem,_cropsContentList[lustIndex]);
                item.Init(_shopService, _shopItemDataList[i],_staticDataService.GetGardenData(_shopItemDataList));
            }
        }
    }
}