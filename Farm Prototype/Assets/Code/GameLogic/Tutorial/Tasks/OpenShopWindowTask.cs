using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/OpenShopWindow", fileName = "New Task")]
    public class OpenShopWindowTask : TutorialTask
    {
        public override void OnStart()
        {
            Debug.Log("Start Open Window");
            Hud.ShopOpened += OnShopOpened; 
            base.OnStart();
        }

        private void OnShopOpened()
        {
            OnComplete();
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            Hud.ShopOpened -= OnShopOpened;
        }
    }
}