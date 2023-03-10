using UnityEngine;

namespace Code.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameInitializer _gameInit;
        private void Awake()
        {
            _gameInit = new GameInitializer();
            _gameInit.Init();
        }
    }
}