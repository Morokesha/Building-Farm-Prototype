using System;
using Code.Data.GardenBedData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopTab
{
    public class ShopUI : MonoBehaviour
    {
        public event Action<SeedType> BuyWheat;
        public event Action<SeedType> BuyRice;

        [SerializeField] private Button _buyWheatBtn;
        [SerializeField] private Button _buyRiceBtn;
        [SerializeField] private Button _hideBtn;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            HideShopMenu();
            
            _buyWheatBtn.onClick.AddListener(CreateWheat);
            _buyRiceBtn.onClick.AddListener(CreateRice);
           _hideBtn.onClick.AddListener(HideShopMenu);
        }

        private void HideShopMenu()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void ActivatedShopMenu()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void CreateWheat()
        {
            BuyWheat?.Invoke(SeedType.Wheat);
            HideShopMenu();
        }
        
        private void CreateRice()
        {
            BuyRice?.Invoke(SeedType.Rice);
            HideShopMenu();
        }
    }
}
