using Code.Data.GardenData;
using Code.Data.ResourceData;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Data.ShopData
{
    [SerializeField]
    public class CropsShopData
    {
        public ProductType ProductType;
        
        [Header("DisplayData")] 
        public Image Logo;

        public string NameItem;

        public ResourceAmountData[] PriceItem;
    }
}