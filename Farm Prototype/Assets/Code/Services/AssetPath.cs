using UnityEngine;

namespace Code.Services
{
    public static class AssetPath
    {
        [Header("Templates Cells")]
        public const string GardenAreaVisual = "Template/GardenTemplate/GardenAreaVisual";
        public const string GardenPath = "Template/GardenTemplate/Garden";
        public const string GridCellPath = "Template/GardenTemplate/GridCell";
        
        [Header("Static Data")]
        public const string GardenTypeHolderPath = "Data/GardenData/GardenTypeHolder";
        public const string ResourceHolderPath = "Data/ResourceData/ResourceHolder";
        public const string ShopItemDataHolderPath = "Data/ShopData/ShopItemHolder";
        
        [Header("UI Templates")]
        public const string SelectedAreaWindowPath = "Template/UI/Selected Area Window";
        public const string ShopUIPath = "Template/UI/ShopUI";
        public const string HudPath = "Template/UI/Hud Window";
    }
}