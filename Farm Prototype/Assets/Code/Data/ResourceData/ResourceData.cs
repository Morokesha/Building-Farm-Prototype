using UnityEngine;

namespace Code.Data.ResourceData
{
    [CreateAssetMenu(menuName = "Data/ResourceData/ResourceTypeData", order = 0)]
    public class ResourceData : ScriptableObject
    {
        public ResourceType Type;
    }
}