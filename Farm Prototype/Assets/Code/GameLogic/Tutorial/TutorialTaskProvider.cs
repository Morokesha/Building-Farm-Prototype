using System.Collections.Generic;
using System.Linq;
using Code.GameLogic.Tutorial.Tasks;
using Code.Services.AssetServices;

namespace Code.GameLogic.Tutorial
{
    public class TutorialTaskProvider
    {
        private readonly IAssetProvider _assetProvider;
        
        private List<TutorialTask> _tutorialTasks;

        public TutorialTaskProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;

            LoadTasks();
        }

        private void LoadTasks()
        {
            _tutorialTasks = _assetProvider.LoadAll<TutorialTask>(AssetPath.TutorialTaskPath)
                .ToList();
        }

        public List<TutorialTask> GetTutorialTasks() => _tutorialTasks;
    }
}