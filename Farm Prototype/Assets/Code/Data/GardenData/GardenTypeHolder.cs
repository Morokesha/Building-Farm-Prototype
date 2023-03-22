using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Data.GardenBedData
{
    [CreateAssetMenu(menuName = "Data/GardenBedData/GardenBedHolder", order = 0)]
    public class GardenTypeHolder : ScriptableObject
    {
        public List<GardenData> List;
    }
}