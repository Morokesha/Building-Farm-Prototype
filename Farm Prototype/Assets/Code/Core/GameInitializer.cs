using System;
using Code.Management;
using Code.Services;
using Object = UnityEngine.Object;

namespace Code.Core
{
    public class GameInitializer
    {
        private IGameFactory _gameFactory;

        public void Init()
        {
            _gameFactory = new GameFactory();

            Shop shop = Object.FindObjectOfType<Shop>();
   
        }
    }
}