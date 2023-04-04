using Code.Management;
using Code.Services;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI.Windows.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TabHandler _tabHandler;

        [SerializeField] 
        private Button _hideBtn;
        [SerializeField] 
        private Button _removeGardenBtn;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private IStaticDataService _staticData;
        private IShopService _shopService;
        private IUIFactory _uiFactory;
        private ConstructionBuilder _constructionBuilder;

        public void Init(IStaticDataService staticData,IShopService shopService,IUIFactory uiFactory,
            ConstructionBuilder constructionBuilder)
        {
            _staticData = staticData;
            _shopService = shopService;
            _uiFactory = uiFactory;
            _constructionBuilder = constructionBuilder;
            
            _tabHandler.Init(_staticData,_uiFactory, _shopService);
            
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
