using Code.Data.GardenData;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.ShopTab
{
    public class InformContainer : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameCrops;
        [SerializeField] 
        private TextMeshProUGUI _informationToProduct;

        public void Init(GardenData gardenData)
        {
            
        } 
    }
}