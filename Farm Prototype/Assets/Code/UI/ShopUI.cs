using System;
using Code.Data.GardenBedData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ShopUI : MonoBehaviour
    {
        public event Action<SeedType> BuyWheat; 
        
        [SerializeField] private Button _buyWheatBtn;
        [SerializeField] private Button _hideBtn;

        private void Awake()
        {
           _buyWheatBtn.onClick.AddListener(CreateWheat);
           _hideBtn.onClick.AddListener(HideShop);
        }

        private void HideShop()
        {
            gameObject.SetActive(false);
        }

        private void CreateWheat()
        {
            BuyWheat?.Invoke(SeedType.Wheat);
            gameObject.SetActive(false);
        }
    }
}
