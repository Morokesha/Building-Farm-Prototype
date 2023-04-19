using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.ShopData
{
    [CreateAssetMenu(menuName = "Data/ShopData/ShopItemDataHolder", order = 0)]
    public class ShopItemDataHolder : ScriptableObject
    {
        public List<ShopItemData> ShopItemDataList;
    }
}