using System.Collections.Generic;
using System.Linq;
using Code.Data.ShopData;
using Code.Services;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.Windows.Shop.WindowElements
{
    public class TabSection : MonoBehaviour
    {
        [SerializeField] 
        private NavigationButtons _navigationButtons;
        [SerializeField] 
        private InformContainer _informContainer;
        [SerializeField] 
        private RectTransform _containerPanels;

        [SerializeField] 
        private Vector2 _originalPanelPosition;
        [SerializeField] 
        private Vector2 _nextPanelPosition;
        [SerializeField] 
        private Vector2 _backPanelPosition;

        private IUIFactory _uiFactory;
        private IShopService _shopService;
        private IStaticDataService _staticDataService;

        private List<ShopItemData> _shopItemDataList;
        private List<ContentItem> _contentItems;
        private List<RectTransform> _panelContentList;
        private List<RectTransform> _upgradeContentList;

        private ShopItemType _shopItemType;

        private int _currentIndexOpenPanel;
        private readonly int _amountItemsOnPanel = 6;

        public void Init(IStaticDataService staticDataService,IUIFactory uiFactory,IShopService shopService)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _shopService = shopService;

            _shopItemType = ShopItemType.Crops;
            
            
            _shopItemDataList = new List<ShopItemData>();
            _contentItems = new List<ContentItem>();
            _panelContentList = new List<RectTransform>();
            _upgradeContentList = new List<RectTransform>();

            CreateSortListShopData();
            AddContentPanel();
            CreateContentItem();
        }

        private void Start()
        {
            _navigationButtons.OnClickNavigation += OnOnClickNavigation;
        }

        

        #region Events Methods
        private void OnOnClickNavigation(NavigationMode navigationMode)
        {
            
        }

        private void ShowInformAboutItem(ShopItemData shopItemData) => 
            _informContainer.Show(shopItemData);

        private void HideInformAboutItem() => 
            _informContainer.Hide();

        #endregion
        
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
                        (_containerPanels);
                    _panelContentList.Add(panelWithContent);
                    _currentIndexOpenPanel = i;
                }
            }

            for (int i = 0; i < _panelContentList.Count; i++)
            {
                if (i > 0)
                {
                    _panelContentList[i].anchoredPosition += _nextPanelPosition;
                    _panelContentList[i].gameObject.SetActive(false);
                }
            }
            
            _navigationButtons.ActiveLeftButton(false);
        }

        private void CreateContentItem()
        {
            int lustIndex = 0;
                
            for (int i = 0; i < _shopItemDataList.Count; i++)
            {
                if (i > 0 && i % _amountItemsOnPanel == 0) 
                    lustIndex++;
                
                ContentItem item = _uiFactory.CreateContentItem(_panelContentList[lustIndex]);
                item.Init(_shopService, _shopItemDataList[i],_staticDataService.
                    GetGardenData(_shopItemDataList[i].ProductType));
                item.SelectedItem += ShowInformAboutItem;
                item.DeselectedItem += HideInformAboutItem;

                _contentItems.Add(item);
            }
        }

        private void PanelSwipeAnimation()
        {
            
        }

        private void OnDestroy()
        {
            foreach (ContentItem item in _contentItems)
            {
                item.SelectedItem -= ShowInformAboutItem;
                item.DeselectedItem -= HideInformAboutItem;
            }
        }
    }
}