using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Data.ShopData
{
    [CreateAssetMenu(menuName = "Data/ShopData/ShopDataHolder", order = 0)]
    public class ShopDataHolder : ScriptableObject
    { 
        public List<CropsShopData> CropsShopDataList;
    }
}