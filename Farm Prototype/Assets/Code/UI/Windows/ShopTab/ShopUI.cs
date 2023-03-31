using System;
using Code.Data.GardenData;
using Code.Management;
using Code.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopTab
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] 
        private Button _hideBtn;
        [SerializeField] 
        private Button _removeGardenBtn;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField] 
        private ShopCropsContent _shopCropsContent;

        private IShopService _shopService;
        private IUIFactory _uiFactory;
        private ConstructionBuilder _constructionBuilder;
        private GardenTypeHolder _gardenTypeHolder;

        public void Init(IShopService shopService,IUIFactory uiFactory,GardenTypeHolder gardenTypeHolder,
            ConstructionBuilder constructionBuilder)
        {
            _shopService = shopService;
            _uiFactory = uiFactory;
            _gardenTypeHolder = gardenTypeHolder;
            _constructionBuilder = constructionBuilder;

            _shopCropsContent.Init(_shopService,_gardenTypeHolder);
            
            HideShopMenu();
            
            _hideBtn.onClick.AddListener(ExitShopMenu);
            _removeGardenBtn.onClick.AddListener(RemoveGarden);
        }


        private void ExitShopMenu()
        {
            _constructionBuilder.SetConstructionState(ConstructionState.Select);
            HideShopMenu();
        }

        private void RemoveGarden()
        {
            _constructionBuilder.ClearGardenAreaVisual();
            _removeGardenBtn.gameObject.SetActive(false);
        }

        private void HideShopMenu()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            _removeGardenBtn.gameObject.SetActive(false);
        }

        public void ActivatedShopMenu()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            
            _constructionBuilder.SetConstructionState(ConstructionState.WaitBuilt);
        }

        private void OnDestroy()
        {
            _hideBtn.onClick.RemoveListener(ExitShopMenu);
            _removeGardenBtn.onClick.RemoveListener(RemoveGarden);
        }
    }
}
