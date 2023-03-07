using UnityEngine.Serialization;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceGeneratorAmount
    {
        public ResourceData resource;
        
        public int MinAmount;
        public int MaxAmount;
    }
}