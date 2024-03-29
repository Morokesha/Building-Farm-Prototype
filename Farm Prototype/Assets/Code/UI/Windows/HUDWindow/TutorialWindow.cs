﻿using Code.GameLogic.Tutorial;
using Code.GameLogic.Tutorial.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.HUDWindow
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tutorialText;
        [SerializeField] private GameObject _completeContent;
        [SerializeField] private Button _closeButton;
        
        private TutorialController _tutorialController;
        
        public void Init(TutorialController tutorialController)
        {
            _tutorialController = tutorialController;
            _tutorialController.TaskStarted += OnNewTaskStarted;
            _tutorialController.TutorialCompleted += OnTutorialCompleted;

            _closeButton.onClick.AddListener(() => Destroy(gameObject));
            _completeContent.gameObject.SetActive(false);
        }

        private void OnNewTaskStarted(TutorialTaskData taskData)
        {
            _tutorialText.SetText(taskData.Description);
        }

        private void OnTutorialCompleted()
        {
            _tutorialText.gameObject.SetActive(false);
            _completeContent.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _tutorialController.TaskStarted -= OnNewTaskStarted;
            _tutorialController.TutorialCompleted -= OnTutorialCompleted;
            
            _tutorialController.CleanUp();
        }
    }
}