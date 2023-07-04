using Code.Data.GardenData;
using Code.Data.ShopData;
using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    [CreateAssetMenu(menuName = "Tutorial/Tasks/BuyWheat", fileName = "New Task")]
    public class BuyWheatTask : TutorialTask
    {
        public override void OnStart()
        {
            ShopService.SoldGarden += OnSoldGarden;
            base.OnStart();
        }

        private void OnSoldGarden(GardenData gardenData, ShopItemData shopItemData)
        {
            if (gardenData.ProductType == ProductType.Wheat && shopItemData.ProductType == ProductType.Wheat)
                OnComplete();
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            ShopService.SoldGarden -= OnSoldGarden;
        }
    }
}