using Code.GameLogic.Gardens;
using Code.UI.Windows.HUDWindow;

namespace Code.Services.GardenHandlerService
{
    public interface IGardenHandlerService
    {
        public void Init(HUD hud);
        void AddGarden(Garden garden);
        void RemoveGarden(Garden garden);
        void ClearAll();
    }
}