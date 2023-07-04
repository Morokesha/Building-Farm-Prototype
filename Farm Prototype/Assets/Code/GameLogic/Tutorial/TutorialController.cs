using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameLogic.Tutorial.Tasks;
using Object = UnityEngine.Object;
using Code.Management;
using Code.Services.AssetServices;
using Code.Services.ProgressServices;
using Code.Services.ShopServices;
using Code.UI.Windows.HUDWindow;

namespace Code.GameLogic.Tutorial
{
    public class TutorialController
    {
         public event Action<TutorialTaskData> TaskStarted;
        public event Action TutorialCompleted;

        private readonly IProgressService _progressService;
        private readonly IShopService _shopService;
        private readonly FarmController _farmController;
        private readonly TutorialTaskProvider _provider;
        private readonly HUD _hud;

        private List<TutorialTask> _allTasks;
        private Queue<TutorialTask> _taskQueue;

        public TutorialController(IProgressService progressService, IAssetProvider assetProvider,
            IShopService shopService,FarmController farmController,HUD hud)
        {
            _progressService = progressService;
            _provider = new TutorialTaskProvider(assetProvider);
            _shopService = shopService;
            _farmController = farmController;
            _hud = hud;
            
            _taskQueue = new Queue<TutorialTask>();
            _allTasks = new List<TutorialTask>();
        }

        public void Init()
        {
            var tasks = _provider.GetTutorialTasks()
                .OrderBy(x => x.TaskData.TaskNumber)
                .ToList();

            foreach (TutorialTask tutorialTask in tasks)
            {
                TutorialTask task = Object.Instantiate(tutorialTask);
                task.Init(_progressService,_shopService,_farmController, _hud);
                task.TaskStarted += OnTaskStarted;
                task.TaskCompleted += OnTaskCompleted;

                _allTasks.Add(task);
                _taskQueue.Enqueue(task);
            }
            
            SetNextTask();
        }

        private void OnTaskStarted(TutorialTaskData taskData) => 
            TaskStarted?.Invoke(taskData);

        private void OnTaskCompleted()
        {
            if (HasTask()) 
                SetNextTask();
            else
                TutorialCompleted?.Invoke();
        }

        private bool HasTask() =>
            _taskQueue.Count > 0;

        private void SetNextTask()
        {
            var task = _taskQueue.Dequeue();
            task.OnStart();
        }

        public void CleanUp()
        {
            foreach (TutorialTask task in _allTasks)
            {
                task.TaskStarted -= OnTaskStarted;
                task.TaskCompleted -= OnTaskCompleted;
            }
            
            _allTasks.Clear();
        }
    }
}