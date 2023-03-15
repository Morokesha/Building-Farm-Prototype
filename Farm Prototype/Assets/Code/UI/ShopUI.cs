using System;
using Code.Data.GardenBedData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public enum ActiveShopState
    {
        Activated,
        Deactivated
    }
    public class ShopUI : MonoBehaviour
    {
        public event Action<SeedType> BuyWheat; 
        
        [SerializeField] private Button _buyWheatBtn;
        [SerializeField] private Button _hideBtn;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ActiveShopState _activeShopState;
        
        private void Awake()
        {
           _buyWheatBtn.onClick.AddListener(CreateWheat);
           _hideBtn.onClick.AddListener(DeactivatedShopMenu);
        }

        private void DeactivatedShopMenu() => 
            ActiveShop(ActiveShopState.Deactivated);

        public void ActiveShop(ActiveShopState state)
        {
            if (state == ActiveShopState.Activated)
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
            else
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        private void CreateWheat()
        {
            BuyWheat?.Invoke(SeedType.Wheat);
            gameObject.SetActive(false);
        }
    }
}
