using System.Collections.Generic;
using Code.Data.GardenData;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Code.Data.ShopData;

namespace Code.UI.Windows.ShopTab
{
    public enum TabType
    {
        Crops,
        Upgrade
    }
    public class TabHandler : MonoBehaviour
    {
        [SerializeField] 
        private Button _cropsBtn;

        [SerializeField] 
        private Button _upgradeBtn;
        
        private TabType _tabType;
        
        private GardenDataHolder _gardenDataHolder;
        private ProductType _productType;

        private List<CropsShopData> _cropsShopDataList;
        private List<RectTransform> _cropsContentList;
        private List<RectTransform> _upgradeContentList;

        public void Init(GardenDataHolder gardenDataHolder)
        {
            _gardenDataHolder = gardenDataHolder;

            _cropsShopDataList = new List<CropsShopData>();
            _cropsContentList = new List<RectTransform>();
            _upgradeContentList = new List<RectTransform>();
        }

        private void CreatedCropsContent(ProductType type)
        {
            if (_productType == type)
            {
                
            }
        }
    }
}