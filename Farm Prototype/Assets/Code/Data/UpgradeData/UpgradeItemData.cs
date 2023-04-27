using Code.Data.ShopData;
using UnityEngine;

namespace Code.Data.UpgradeData
{
    [CreateAssetMenu(menuName = "Data/UpgradeData/UpgradeItemData", order = 0)]
    public class UpgradeItemData : ScriptableObject
    {
        public UpgradeType UpgradeType;
        public UpgradeStage UpgradeStage;

        public Sprite SpriteImage;
        public string NameUpgrade;
        
        [Space(4)]
        [Header("Information Text")]
        [TextArea]
        public string DescriptionUpgrade;
        [Header("Price Data")]
        public PriceData PriceData;
    }

    public enum UpgradeType
    {
        Watering,
        Harvesting,
        Expansion,
        Demolition
    }
    
    public enum UpgradeStage
    {
        First,
        Second,
        Third
    }
}