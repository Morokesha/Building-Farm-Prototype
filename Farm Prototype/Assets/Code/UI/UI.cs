using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        [SerializeField] private Button _shopMenuBtn;

        private void Start()
        {
            _shopMenuBtn.onClick.AddListener(ShowMenu);
        }

        private void ShowMenu()
        {
            _shopMenu.SetActive(true);
        }
    }
}