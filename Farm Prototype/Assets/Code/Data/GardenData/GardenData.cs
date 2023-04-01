using Code.Data.ResourceData;
using Code.Data.ShopData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Data.GardenData
{
    [CreateAssetMenu(menuName = "Data/GardenData/GardenData", order = 0)]
    public class GardenData : ScriptableObject
    {
        [FormerlySerializedAs("productType")] public ProductType ProductType;
        public Color colorCrops;

        public string NameGarden;
        
        public int TimeGrowing;

        [Header("DropData")] 
        public DropData DropData;
    }
}