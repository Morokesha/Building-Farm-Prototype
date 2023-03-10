using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;

namespace Code.Services
{
    public interface IAssetProvider
    {
        Garden Garden { get; }
        CellPlanting CellPlanting { get; }
        GardenTypeHolder GardenTypeHolder { get; }
        ResourceHolder ResourceHolder { get; }
    }
}