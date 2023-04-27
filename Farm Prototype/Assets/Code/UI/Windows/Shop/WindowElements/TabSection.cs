using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.FactoryServices;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
using UnityEngine;
using DG.Tweening;
using ShopItemData = Code.Data.ShopData.ShopItemData;

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
        private List<ShopItemData> _shopItemUpgradeList;
        private List<ContentItem> _contentItems;
        private List<RectTransform> _cropsContentList;
        private List<RectTransform> _upgradeContentList;

        private ShopItemType _shopItemType;

        private int _currentIndexOpenPanel;
        private readonly int _amountItemsOnPanel = 6;

        public void Init(IStaticDataService staticDataService,IUIFactory uiFactory,IShopService shopService)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _shopService = shopService;
            
            _shopItemDataList = new List<ShopItemData>();
            _shopItemUpgradeList = new List<ShopItemData>();
            _contentItems = new List<ContentItem>();
            _cropsContentList = new List<RectTransform>();
            _upgradeContentList = new List<RectTransform>();

            CreateSortListShopData();
        }

        private void Start()
        {
            _navigationButtons.OnClickNavigation += OnOnClickNavigation;
            ChangeOpenedTabSection(_shopItemType);
        }

        public void ActiveShopSection(ShopItemType shopItemType)
        {
            _shopItemType = shopItemType;
            _currentIndexOpenPanel = 0;
            ChangeOpenedTabSection(_shopItemType);
        }

        private void ChangeOpenedTabSection(ShopItemType shopItemType)
        {
            switch (shopItemType)
            {
                case ShopItemType.Crops:
                    ActivatedSelectedSection(_cropsContentList, _upgradeContentList);
                    CheckNavigationButtons(_cropsContentList);
                    break;
                case ShopItemType.Upgrade:
                    ActivatedSelectedSection(_upgradeContentList, _cropsContentList);
                    CheckNavigationButtons(_upgradeContentList);
                    break;
            }
        }

        #region Events Methods

        private void OnOnClickNavigation(NavigationMode navigationMode)
        {
            PanelSwipeAnimation(navigationMode);
        }

        private void ShowInformAboutItem(ShopItemData shopItemData) => 
            _informContainer.Show(shopItemData);
        private void ShowInformAboutItem(UpgradeItemData upgradeItemData) => 
            _informContainer.Show(upgradeItemData);

        private void HideInformAboutItem() => 
            _informContainer.Hide();

        #endregion

        private void CreateSortListShopData()
        {
            _shopItemDataList = _staticDataService.LoadShopItemDataForType(ShopItemType.Crops).
                OrderBy(x => x.PriceData.GoldAmount).ToList();
            AddContentPanel(_shopItemDataList, _cropsContentList);
            CreateContentItem(_shopItemDataList, _cropsContentList);
            
            _shopItemUpgradeList = _staticDataService.LoadShopItemDataForType(ShopItemType.Upgrade).
                OrderBy(x => x.PriceData.GoldAmount).ToList();
            AddContentPanel(_shopItemUpgradeList, _upgradeContentList);
            CreateContentItem(_shopItemUpgradeList, _upgradeContentList);
        }

        private void AddContentPanel(List<ShopItemData> shopItemList, List<RectTransform> panelList)
        {
            for (int i = 0; i < shopItemList.Count; i++)
            {
                if (i % _amountItemsOnPanel == 0)
                {
                    RectTransform panelWithContent = _uiFactory.CreateNewPanel
                        (_containerPanels);
                    panelList.Add(panelWithContent);
                }
            }
            
            AddOffsetPanel(panelList);
        }

        private void AddOffsetPanel(List<RectTransform> panelList)
        {
            for (int i = 0; i < panelList.Count; i++)
            {
                if (i > 0)
                {
                    panelList[i].anchoredPosition += _nextPanelPosition;
                    panelList[i].gameObject.SetActive(false);
                }
            }
        }

        private void CreateContentItem(List<ShopItemData> dataList, List<RectTransform> contentList)
        {
            int lustIndex = 0;
                
            for (int i = 0; i < dataList.Count; i++)
            {
                if (i > 0 && i % _amountItemsOnPanel == 0) 
                    lustIndex++;
                
                ContentItem item = _uiFactory.CreateContentItem(contentList[lustIndex]);
                item.Init(_shopService, dataList[i],_staticDataService.
                    GetGardenData(dataList[i].ProductType));
                item.SelectedItem += ShowInformAboutItem;
                item.SelectedUpgradeItem += ShowInformAboutItem;
                item.DeselectedItem += HideInformAboutItem;

                if (dataList[i].ShopItemType == ShopItemType.Upgrade)
                    item.SetUpgradeData(_staticDataService.GetUpgradeData(dataList[i].UpgradeType, 0));
                
                _contentItems.Add(item);
            }
        }

        private void PanelSwipeAnimation(NavigationMode navigationMode)
        {
            int nextIndex = _currentIndexOpenPanel + 1;
            int prevIndex = _currentIndexOpenPanel - 1;

            List<RectTransform> sectionsList = _shopItemType == ShopItemType.Crops ? 
                _cropsContentList : _upgradeContentList;
            
            switch (navigationMode)
            {
                case NavigationMode.Forward:
                {
                    if (_currentIndexOpenPanel < sectionsList.Count - 1) 
                        SwipeAnimation(sectionsList,_backPanelPosition,nextIndex);
                    break;
                }
                case NavigationMode.Back:
                {
                    if (_currentIndexOpenPanel > 0 && _currentIndexOpenPanel != 0)
                        SwipeAnimation(sectionsList,_nextPanelPosition, prevIndex);
                    break;
                }
            }
            
            CheckNavigationButtons(sectionsList);
        }

        private void SwipeAnimation(List<RectTransform> listPanels,Vector2 newPanelPosition,int index)
        {
            listPanels[_currentIndexOpenPanel].DOAnchorPos(newPanelPosition, 0.4f);
            listPanels[_currentIndexOpenPanel].gameObject.SetActive(false);
            listPanels[index].gameObject.SetActive(true);
            listPanels[index].DOAnchorPos(_originalPanelPosition, 0.4f);
            _currentIndexOpenPanel = index;
        }

        private void CheckNavigationButtons(List<RectTransform> contentList)
        {
            if (_currentIndexOpenPanel == 0)
            {
                _navigationButtons.ActiveLeftButton(false);
                _navigationButtons.ActiveRightButton(true);
            }
            if (_currentIndexOpenPanel == contentList.Count-1)
            {
                _navigationButtons.ActiveRightButton(false);
                _navigationButtons.ActiveLeftButton(true);
            }
            if(_currentIndexOpenPanel > 0 && _currentIndexOpenPanel < contentList.Count-1)
            {
                _navigationButtons.ActiveLeftButton(true);
                _navigationButtons.ActiveRightButton(true);
            }
            if(_currentIndexOpenPanel == 0 && contentList.Count == 1)
            {
                _navigationButtons.ActiveLeftButton(false);
                _navigationButtons.ActiveRightButton(false);
            }
        }

        private void ActivatedSelectedSection(List<RectTransform> activeSectionList, 
            List<RectTransform> deactivatedSectionList)
        {
            ResetPositionPanel(activeSectionList);
            activeSectionList[0].gameObject.SetActive(true);
            foreach (RectTransform section in deactivatedSectionList) 
                section.gameObject.SetActive(false);
        }

        private void ResetPositionPanel(List<RectTransform> activeSection)
        {
            for (int i = 0; i < activeSection.Count; i++)
            {
                if (i != 0) 
                    activeSection[i].anchoredPosition = _originalPanelPosition + _nextPanelPosition;
                else
                    activeSection[i].anchoredPosition = _originalPanelPosition;
            }
        }

        private void OnDestroy()
        {
            foreach (ContentItem item in _contentItems)
            {
                item.SelectedItem -= ShowInformAboutItem;
                item.SelectedUpgradeItem -= ShowInformAboutItem;
                item.DeselectedItem -= HideInformAboutItem;
            }
        }
    }
}