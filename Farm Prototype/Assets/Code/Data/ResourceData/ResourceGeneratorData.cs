using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceGeneratorData
    {
        public float TimeGrowingCrops;
        [Range(1,30)]
        public int CoinAmout;
        [Range(1,5)]
        public int SeedAmount;
    }
}