using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.ShopData
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class CropsDataHolder : ScriptableObject
    {
        public List<CropsShopData> CropsDataList;
    }
}