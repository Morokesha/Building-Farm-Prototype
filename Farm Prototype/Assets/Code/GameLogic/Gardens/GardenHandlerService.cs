using System.Collections.Generic;
using Code.Data.ResourceData;
using Code.Services.GardenHandlerService;
using Code.UI;

namespace Code.GameLogic.Gardens
{
    public class GardenHandler : IGardenHandlerService
    {
        private List<Garden> _gardens;
        
        private HUD _hud;

        public void Init(HUD hud)
        {
            _gardens = new List<Garden>();

            _hud = hud;
            _hud.ClickWateringAll += OnClickWateringAll;
            _hud.ClickHarvestingAll += OnClickHarvestingAll;
        }

        private void OnClickHarvestingAll()
        {
            foreach (Garden garden in _gardens)
            {
                GardenProduction gardenProduction = garden.GetGardenProduction();
                
                if (gardenProduction.GetProductionState() == ProductionState.CompleteGrowth)
                {
                    if (gardenProduction.GetHarvestingResourceType() == ResourceType.Gold)
                        gardenProduction.Harvesting(ResourceType.Gold);
                    if (gardenProduction.GetHarvestingResourceType() == ResourceType.Seed)
                        gardenProduction.Harvesting(ResourceType.Seed);
                }
            }
        }

        private void OnClickWateringAll()
        {
            foreach (Garden garden in _gardens)
            {
                GardenProduction gardenProduction = garden.GetGardenProduction();

                if (gardenProduction.GetProductionState() == ProductionState.WaitWatering) 
                    gardenProduction.Growing();
            }
        }

        public void AddGarden(Garden garden) => 
            _gardens.Add(garden);
        public void RemoveGarden(Garden garden) => 
            _gardens.Remove(garden);

        public void ClearAll()
        {
            _hud.ClickWateringAll -= OnClickWateringAll;
            _hud.ClickHarvestingAll -= OnClickHarvestingAll;
        }
    }
}