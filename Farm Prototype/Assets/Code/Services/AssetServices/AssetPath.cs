using UnityEngine;

namespace Code.Services.AssetServices
{
    public static class AssetPath
    {
        [Header("Templates Cells")]
        public const string GardenAreaVisual = "Template/GardenTemplate/GardenAreaVisual";
        public const string GardenPath = "Template/GardenTemplate/Garden";
        public const string GridCellPath = "Template/GardenTemplate/GridCell";
        
        [Header("Static Data")]
        public const string GardenTypeHolderPath = "Data/GardenData/Holder/GardenTypeHolder";
        public const string ResourceHolderPath = "Data/ResourceData/ResourceHolder";
        public const string ShopItemDataHolderPath = "Data/ShopData/ShopItemHolder";
        public const string UpgradeDataPath = "Data/UpgradeData";


        [Header("UI Templates")]
        public const string SelectedAreaWindowPath = "Template/UI/Selected Area Window";
        public const string ShopUIPath = "Template/UI/ShopUI";
        public const string HudPath = "Template/UI/Hud Window";
        public const string ContentItemPath = "Template/UI/ContentItem";
        public const string ContentPanelPath = "Template/UI/ContentPanel";
    }
}