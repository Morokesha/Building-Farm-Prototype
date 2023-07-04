using System;
using Code.Management;
using Code.Services.ProgressServices;
using Code.Services.ShopServices;
using Code.UI.Windows.HUDWindow;
using UnityEngine;

namespace Code.GameLogic.Tutorial.Tasks
{
    public abstract class TutorialTask : ScriptableObject
    {
        public event Action<TutorialTaskData> TaskStarted;
        public event Action TaskCompleted;

        public TutorialTaskData TaskData;

        protected IProgressService ProgressService;
        protected FarmController FarmController;
        protected IShopService ShopService;
        protected HUD Hud;

        public void Init(IProgressService progressService,IShopService shopService,
            FarmController farmController, HUD hud)
        {
            ProgressService = progressService;
            ShopService = shopService;
            FarmController = farmController;
            Hud = hud;
        }

        public virtual void OnStart() => 
            TaskStarted?.Invoke(TaskData);

        protected virtual void OnComplete()
        {
            TaskCompleted?.Invoke();
            Debug.Log("Task Completed");
        }
    }

    [Serializable]
    public class TutorialTaskData
    {
        [TextArea]
        public string Description;
        public int TaskNumber;
        public bool IsLastTask = false;
    }
}