using Code.Data.ShopData;
using UnityEngine;

namespace Code.Data.ResourceData
{
    [CreateAssetMenu(menuName = "Data/ResourceData/ResourceHolder", order = 0)]
    public class ResourceHolder : ScriptableObject
    {
        public PriceData StartResource;
    }
}