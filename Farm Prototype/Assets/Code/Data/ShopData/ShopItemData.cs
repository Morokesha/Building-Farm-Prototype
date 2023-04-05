using Code.Data.GardenData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Data.ShopData
{
    [CreateAssetMenu(menuName = "Data/ShopData/ShopItemData", order = 0)]
    public class ShopItemData : ScriptableObject
    {
    public ProductType ProductType;
    public ShopItemType ShopItemType;

    [Header("DisplayData")] 
    public Sprite Logo;

    public string NameItem;
    [Space(4)]
    [TextArea]
    public string InformationAboutItem;

    [Header("Price Data")]
    [Space(4)]
    public PriceData PriceData;
    }
    
    public enum ShopItemType
    {
        Crops,
        Upgrade
    }
    
}