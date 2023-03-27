﻿using System.Net.Mime;
using Code.Data.ResourceData;
using UnityEngine;

namespace Code.Data.GardenBedData
{
    [CreateAssetMenu(menuName = "Data/SeedData/SeedTypeData", order = 0)]
    public class GardenData : ScriptableObject
    {
        public SeedType SeedType;
        public string GardenName;
        public Color colorCrops;
        public ResourceGeneratorData GeneratorData;
        public ResourceAmountData[] GardenCostArray;
    }
}