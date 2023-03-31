using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceGeneratorData
    {
        public float TimeGrowingCrops;

        public int SeedDropChanse;
        
        [Range(1,30)]
        public int GoldAmout;
        [Range(1,5)]
        public int SeedAmount;
    }
}