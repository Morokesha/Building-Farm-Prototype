using Code.Data.ShopData;
using Code.Services;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop.WindowElements
{
    public class TabHandler : MonoBehaviour
    {
        [SerializeField] 
        private TabSection _tabSection;
        
        [SerializeField] 
        private Button _cropsBtn;

        [SerializeField] 
        private Button _upgradeBtn;
        
        private ShopItemType _shopItemType;

        private IStaticDataService _staticDataService;
        private IUIFactory _uiFactory;
        private IShopService _shopService;


        public void Init(IStaticDataService staticDataService, IUIFactory uiFactory, IShopService shopService)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _shopService = shopService;

            _tabSection.Init(_staticDataService,_uiFactory,_shopService);
            
            _shopItemType = ShopItemType.Crops;
            
            _cropsBtn.onClick.AddListener(OnCropClick);
            _upgradeBtn.onClick.AddListener(OnUpgradeClick);
        }
        

        private void OnCropClick() => 
            _shopItemType = ShopItemType.Crops;

        private void OnUpgradeClick() => 
            _shopItemType = ShopItemType.Upgrade;

        private void OnDestroy()
        {
            _cropsBtn.onClick.RemoveListener(OnCropClick);
            _upgradeBtn.onClick.RemoveListener(OnUpgradeClick);
        }
    }
}