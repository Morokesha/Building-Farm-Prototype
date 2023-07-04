using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/BuyImprovement", fileName = "New Task")]
    public class BuyImprovementTask : TutorialTask
    {
        public override void OnStart()
        {
            ProgressService.SickleActivated += OnSickleActivated;
            base.OnStart();
        }

        private void OnSickleActivated(bool active)
        {
            if (active) 
                OnComplete();
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            ProgressService.SickleActivated -= OnSickleActivated;
        }
    }
}