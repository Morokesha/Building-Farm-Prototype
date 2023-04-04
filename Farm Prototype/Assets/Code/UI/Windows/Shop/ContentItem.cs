using Code.Data.GardenData;
using Code.Data.ShopData;
using Code.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop
{
    public class ContentItem : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameItem;
        [SerializeField] 
        private TextMeshProUGUI _cost;
        [SerializeField] 
        private Image _logoItem;

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

            _itemBtn.onClick.AddListener(BuyGarden);
            
            FillWithContent();
        }

        private void FillWithContent()
        {
            _nameItem.text = _shopItemData.NameItem;
            _logoItem.sprite = _shopItemData.Logo;
            _cost.text = "Gold " + _shopItemData.PriceData.GoldAmount
                            + " Seed " + _shopItemData.PriceData.SeedAmount;
        }

        private void BuyGarden() => 
            _shopService.BuyGarden(_gardenData);

        private void OnDestroy() => 
            _itemBtn.onClick.RemoveListener(BuyGarden);
    }
}