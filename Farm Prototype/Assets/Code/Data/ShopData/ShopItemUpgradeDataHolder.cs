using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.ShopData
{
    [CreateAssetMenu(menuName = "Data/ShopData/ShopItemUpgradeDataHolder", order = 0)]
    public class ShopItemUpgradeDataHolder : ScriptableObject
    {
        public List<ShopItemData> UpgradeItemList;
    }
}