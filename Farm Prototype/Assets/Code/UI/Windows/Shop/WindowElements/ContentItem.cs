using System;
using Code.Common;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using Code.Services.ShopServices;
using Code.Services.StaticDataServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop.WindowElements
{
    public class ContentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<ShopItemData> SelectedItem; 
        public event Action<UpgradeItemData> SelectedUpgradeItem; 
        public event Action DeselectedItem;

        [SerializeField] 
        private TextMeshProUGUI _nameItem;
        [SerializeField] 
        private TextMeshProUGUI _cost;
        [SerializeField] 
        private Image _logoItem;
        [SerializeField] 
        private Outline _backgroundOutline;

        [SerializeField] 
        private Button _itemBtn;

        [SerializeField] 
        private CanvasGroup _canvasGroup;

        private IStaticDataService _staticDataService;
        private IShopService _shopService;
        private ShopItemData _shopItemData;
        private GardenData _gardenData;
        private UpgradeItemData _upgradeData;

        public UpgradeItemData GetUpgradeItemData => _upgradeData;
        public void Init(IShopService shopService,
            ShopItemData shopItemData,GardenData gardenData)
        {
            _shopService = shopService;
            _shopItemData = shopItemData;
            _gardenData = gardenData;
            
            UpdateContentItem(_shopItemData);
            ActiveBackgroundOutline(false);

            _itemBtn.onClick.AddListener(BuyProduct);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ActiveBackgroundOutline(true);

            if (_shopItemData.ShopItemType == ShopItemType.Crops) 
                SelectedItem?.Invoke(_shopItemData);
            else
                SelectedUpgradeItem?.Invoke(_upgradeData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ActiveBackgroundOutline(false);
            DeselectedItem?.Invoke();
        }

        public void SetUpgradeData(UpgradeItemData upgradeItemData)
        {
            _upgradeData = upgradeItemData;
            UpdateContentItem(_upgradeData);
        }

        public void Hide() => 
            _canvasGroup.SetActive(false);

        private void UpdateContentItem(UpgradeItemData upgradeItemData)
        {
            _nameItem.text = upgradeItemData.NameUpgrade;
            _logoItem.sprite = upgradeItemData.SpriteImage;
            _cost.text = upgradeItemData.PriceData.GoldAmount + " Gold "
                                                              + upgradeItemData.PriceData.SeedAmount + " Seed";
        }
        
        private void UpdateContentItem(ShopItemData shopItemData)
        {
            _nameItem.text = shopItemData.NameItem;
            _logoItem.sprite = shopItemData.Sprite;
            _cost.text = shopItemData.PriceData.GoldAmount + " Gold "
                                                           + shopItemData.PriceData.SeedAmount + " Seed";
        }

        private void ActiveBackgroundOutline(bool active) => 
            _backgroundOutline.enabled = active;

        private void BuyProduct()
        {
            switch (_shopItemData.ShopItemType)
            {
                case ShopItemType.Crops:
                    _shopService.BuyGarden(_shopItemData, _gardenData);
                    break;
                case ShopItemType.Upgrade:
                    _shopService.BuyUpgrade(_upgradeData,this);
                    break;
            }
        }

        private void OnDestroy() => 
            _itemBtn.onClick.RemoveListener(BuyProduct);
    }
}