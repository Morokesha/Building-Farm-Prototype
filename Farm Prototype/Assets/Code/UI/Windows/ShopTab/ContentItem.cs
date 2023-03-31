using System;
using Code.Data.GardenData;
using Code.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopTab
{
    public class ContentItem : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameItem;
        [SerializeField] 
        private TextMeshProUGUI _goldText;
        [SerializeField] 
        private TextMeshProUGUI _SeedText;
        [SerializeField] 
        private Image _logoItem;

        [SerializeField] 
        private Button _itemBtn;

        private IShopService _shopService;
        private GardenData _gardenData;

        private void Init(GardenData gardenData, IShopService shopService)
        {
            _gardenData = gardenData;
            _shopService = shopService;
            
            _itemBtn.onClick.AddListener(BuyGarden);
        }

        private void BuyGarden() => 
            _shopService.BuyGarden(_gardenData.productType);

        private void OnDestroy() => 
            _itemBtn.onClick.RemoveListener(BuyGarden);
    }
}