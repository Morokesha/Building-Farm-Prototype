using System;
using Code.Data.ResourceData;

namespace Code.Services
{
    public interface IProgressDataService
    {
        public event Action<ResourceType, int> ResourceChanded; 
        void AddResources(ResourceType type, int amount);
    }
}