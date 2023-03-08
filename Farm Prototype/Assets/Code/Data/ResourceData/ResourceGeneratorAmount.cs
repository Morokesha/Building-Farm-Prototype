using UnityEngine;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceGeneratorAmount
    {
        public ResourceData ResourceData;
        
        [Range(0,3)]
        public int MinAmount;
        [Range(4,15)]
        public int MaxAmount;
    }
}