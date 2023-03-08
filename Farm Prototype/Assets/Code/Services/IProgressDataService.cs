using Code.Data.ResourceData;

namespace Code.Services
{
    public interface IProgressDataService
    {
        void AddResources(ResourceType type, int amount);
        int GetCoinCount();
        int GetSeedCount();
    }
}