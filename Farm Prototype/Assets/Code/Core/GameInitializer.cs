using System;
using Code.Management;
using Code.Services;
using Object = UnityEngine.Object;

namespace Code.Core
{
    public class GameInitializer
    {
        private IGameFactory _gameFactory;
        private IProgressDataService _progressDataService;

        public void Init()
        {
            _progressDataService = new ProgressDataService();
            _gameFactory = new GameFactory();
        }
    }
}