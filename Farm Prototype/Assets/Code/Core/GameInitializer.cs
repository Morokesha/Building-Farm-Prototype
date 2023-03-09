using Code.Management;
using Code.Services;

namespace Code.Core
{
    public class GameInitializer
    {
        private IProgressDataService _progressDataService;
        private IGameFactory _gameFactory;
        private IResourceService _resourceService;

        public void Init()
        {
            _progressDataService = new ProgressDataService();
            _gameFactory = new GameFactory();
            _resourceService = new ResourceRepository();
        }
    }
}