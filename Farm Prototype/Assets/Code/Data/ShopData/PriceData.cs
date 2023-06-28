using System;
using UnityEngine;

namespace Code.Data.ShopData
{
    [Serializable]
    public class PriceData
    {
        [Range(0,2500)]
        public int GoldAmount;

        [Range(0, 30)] 
        public int SeedAmount;
    }
}