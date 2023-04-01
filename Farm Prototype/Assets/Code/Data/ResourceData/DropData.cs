using UnityEngine;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class DropData
    {
        public int SeedDropChance;
        [Range(0,25)]
        public int GoldAmount;

        [Range(0, 5)] 
        public int SeedAmount;
    }
}