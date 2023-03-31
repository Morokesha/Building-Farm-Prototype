using Code.Data.ResourceData;
using Code.Data.ShopData;
using UnityEngine;

namespace Code.Data.GardenData
{
    [CreateAssetMenu(menuName = "Data/GardenData/GardenData", order = 0)]
    public class GardenData : ScriptableObject
    {
        public ProductType productType;
        public Color colorCrops;

        [Header("DropData")] 
        public ResourceGeneratorData DropData;

        [Header("Crops Shop Data")] 
        public CropsShopData CropsShopData;
    }
}