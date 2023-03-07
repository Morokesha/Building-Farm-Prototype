using UnityEngine;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceAmount
    {
        public ResourceData ResourceData;

        [Range(0,25)]
        public int Amount;
        
        public int MinAmount;
        public int MaxAmount;
    }
}