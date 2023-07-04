using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/PlantWheat", fileName = "New Task")]
    public class PlantWheatTask : TutorialTask
    {
        public override void OnStart()
        {
            FarmController.PlantGarden += OnPlantGarden; 
            base.OnStart();
        }

        private void OnPlantGarden()
        {
            OnComplete();
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            FarmController.PlantGarden -= OnPlantGarden;
        }
    }
}