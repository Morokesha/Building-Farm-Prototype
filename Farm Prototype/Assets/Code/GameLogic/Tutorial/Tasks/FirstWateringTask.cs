using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/FirstWatering", fileName = "New Task")]
    public class FirstWateringTask : TutorialTask
    {
        public override void OnStart()
        {
            Hud.SelectedAreaWindow.FirstWatering += OnFirstWatering;
            base.OnStart();
        }

        private void OnFirstWatering() => 
            OnComplete();

        protected override void OnComplete()
        {
            base.OnComplete();
            Hud.SelectedAreaWindow.FirstWatering -= OnFirstWatering;
        }
    }
}