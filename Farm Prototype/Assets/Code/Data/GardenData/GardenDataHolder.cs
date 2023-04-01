using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.GardenData
{
    [CreateAssetMenu(menuName = "Data/GardenData/GardenHolder", order = 0)]
    public class GardenDataHolder : ScriptableObject
    {
        public List<GardenData> List;
    }
}