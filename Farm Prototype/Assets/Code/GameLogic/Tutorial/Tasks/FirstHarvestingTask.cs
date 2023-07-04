using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/FirstHarvesting", fileName = "New Task")]
    public class FirstHarvestingTask : TutorialTask
    {
        public override void OnStart()
        {
            Hud.SelectedAreaWindow.FirstHarvesting += OnFirstHarvesting;
            base.OnStart();
        }

        private void OnFirstHarvesting() => 
            OnComplete();

        protected override void OnComplete()
        {
            base.OnComplete();
            Hud.SelectedAreaWindow.FirstHarvesting -= OnFirstHarvesting;
        }
    }
}