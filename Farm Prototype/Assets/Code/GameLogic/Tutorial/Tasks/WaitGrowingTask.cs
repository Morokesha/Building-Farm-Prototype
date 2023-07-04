using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/WaitGrowing", fileName = "New Task")]
    public class WaitGrowingTask: TutorialTask
    {
        public override void OnStart()
        {
            Hud.SelectedAreaWindow.GrowingCompleted += OnGrowingCompleted;
            base.OnStart();
        }

        private void OnGrowingCompleted()
        {
            OnComplete();
        }


        protected override void OnComplete()
        {
            base.OnComplete();
            Hud.SelectedAreaWindow.GrowingCompleted -= OnGrowingCompleted;
        }
    }
}