using Code.Common;
using Code.Data.ShopData;
using Code.Data.UpgradeData;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.Shop.WindowElements
{
    public class InformContainer : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameItem;
        [SerializeField] 
        private TextMeshProUGUI _informationArea;
        [SerializeField] 
        private CanvasGroup _canvasGroup;

        private void Start() => 
            Hide();

        public void Show(ShopItemData shopItemData)
        {
            _canvasGroup.SetActive(true);
            SetDescription(shopItemData);
        }

        public void Show(UpgradeItemData upgradeItemData)
        {
            _canvasGroup.SetActive(true);
            SetDescription(upgradeItemData);
        }

        public void Hide() => 
            _canvasGroup.SetActive(false);

        private void SetDescription(ShopItemData itemData)
        {
            _nameItem.SetText(itemData.NameItem);
            _informationArea.SetText(itemData.InformationAboutItem);
        }
        private void SetDescription(UpgradeItemData upgradeItemData)
        {
            _nameItem.SetText(upgradeItemData.NameUpgrade);
            _informationArea.SetText(upgradeItemData.DescriptionUpgrade);
        }
    }
}