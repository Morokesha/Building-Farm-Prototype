using UnityEngine;

namespace Code.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameInitializer _gameInit;
        private void Start()
        {
            _gameInit = new GameInitializer();
            _gameInit.Init();
        }
    }
}