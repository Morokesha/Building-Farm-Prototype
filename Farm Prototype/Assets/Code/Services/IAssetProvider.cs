using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;

namespace Code.Services
{
    public interface IAssetProvider
    {
        Garden Garden { get; }
        GridSell GridSell { get; }
        GardenTypeHolder GardenTypeHolder { get; }
        ResourceHolder ResourceHolder { get; }
        GardenInfoUI GardenInfoUI { get; }
        ShopUI ShopUI { get; }
        HUD HUD { get; }
    }
}