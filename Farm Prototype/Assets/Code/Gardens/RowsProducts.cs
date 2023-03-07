using Code.Data.GardenBedData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gardens
{
    public class RowsProducts : MonoBehaviour
    {
        [FormerlySerializedAs("SemenType")] public SeedType seedType;
    }
}