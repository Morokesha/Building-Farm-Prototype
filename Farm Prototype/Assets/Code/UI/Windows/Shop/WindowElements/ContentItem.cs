using System;
using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Services;
using Code.Services.ShopServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Code.UI.Windows.Shop.WindowElements
{
    public class ContentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<ShopItemData> SelectedItem; 
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

        private IShopService _shopService;
        private ShopItemData _shopItemData;
        private GardenData _gardenData;

        public void Init(IShopService shopService,ShopItemData shopItemData,GardenData gardenData)
        {
            _shopService = shopService;
            _shopItemData = shopItemData;
            _gardenData = gardenData;

            FillWithContent();
            ActiveBackgroundOutline(false);

            _itemBtn.onClick.AddListener(BuyProduct);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ActiveBackgroundOutline(true);
            SelectedItem?.Invoke(_shopItemData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ActiveBackgroundOutline(false);
            DeselectedItem?.Invoke();
        }

        private void FillWithContent()
        {
            _nameItem.text = _shopItemData.NameItem;
            _logoItem.sprite = _shopItemData.Logo;
            _cost.text = _shopItemData.PriceData.GoldAmount + " Gold "
                            + _shopItemData.PriceData.SeedAmount + " Seed";
        }

        private void ActiveBackgroundOutline(bool active) => 
            _backgroundOutline.enabled = active;

        private void BuyProduct()
        {
            if (_shopItemData.ShopItemType == ShopItemType.Crops)
            {
                _shopService.BuyGarden(_shopItemData, _gardenData);
            }
        }

        private void OnDestroy() => 
            _itemBtn.onClick.RemoveListener(BuyProduct);
    }
}