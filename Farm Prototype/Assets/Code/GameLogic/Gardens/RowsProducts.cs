using Code.Data.GardenBedData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.GameLogic.Gardens
{
    public class RowsProducts : MonoBehaviour
    {
        [FormerlySerializedAs("SemenType")] public SeedType seedType;
    }
}