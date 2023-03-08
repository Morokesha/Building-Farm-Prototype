using UnityEngine;

namespace Code.Data.ResourceData
{
    [System.Serializable]
    public class ResourceAmountData
    {
        public ResourceData ResourceData;

        [Range(0,25)]
        public int Amount;
        
    }
}