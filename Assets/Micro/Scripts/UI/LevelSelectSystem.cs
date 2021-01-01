using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class LevelSelectSystem : MonoBehaviour
    {
        public Action<PlayData.LevelType> OnLevelSelectClicked;

        public SelectionLevelController learnerLevelController;
        public SelectionLevelController movementLevelController;
        public SelectionLevelController focusLevelController;
        public SelectionLevelController awarenessLevelController;
        public SelectionLevelController masteryLevelController;


        public FadePanelController fadePanelController;

        private GameData gameData;
        private PlayData.LevelType levelType;

        public void Init(GameData pGameData)
        {
            gameData = pGameData;

            learnerLevelController.OnClicked += OnLearnerLevelClicked;
            learnerLevelController.EnableInput(false);
            learnerLevelController.UpdateLevelIndex(pGameData.levelLearnerData.levelIndex, pGameData.levelLearnerData.levelCount);
            
            if (pGameData.levelLearnerData.unlocked)
            {
                learnerLevelController.UnlockLevelArea();
                learnerLevelController.EnableInput(true);

                if (pGameData.levelLearnerData.completed)
                {
                    learnerLevelController.CompleteLevelArea();
                    learnerLevelController.EnableInput(false);
                }
            }

            movementLevelController.OnClicked += OnMovementLevelClicked;
            movementLevelController.EnableInput(false);

            focusLevelController.OnClicked += OnFocusLevelClicked;
            focusLevelController.EnableInput(false);

            awarenessLevelController.OnClicked += OnAwarenessLevelClicked;
            awarenessLevelController.EnableInput(false);

            masteryLevelController.OnClicked += OnMasteryLevelClicked;
            masteryLevelController.EnableInput(false);

            fadePanelController.OnFadeComplete += OnFadeOutComplete;
            fadePanelController.FadeOut();
        }

        private void OnFadeOutComplete()
        {
            fadePanelController.OnFadeComplete -= OnFadeOutComplete;
        }

        private void OnLearnerLevelClicked(PlayData.LevelType pLevelType)
        {
            levelType = pLevelType;

            fadePanelController.OnFadeComplete += OnLearnerFadeInComplete;
            fadePanelController.FadeIn();
        }

        private void OnLearnerFadeInComplete()
        {
            CleanupListeners();

            fadePanelController.OnFadeComplete -= OnLearnerFadeInComplete;
            OnLevelSelectClicked?.Invoke(levelType);
        }

        private void OnMovementLevelClicked(PlayData.LevelType pLevelType)
        {

        }

        private void OnFocusLevelClicked(PlayData.LevelType pLevelType)
        {

        }

        private void OnAwarenessLevelClicked(PlayData.LevelType pLevelType)
        {

        }

        private void OnMasteryLevelClicked(PlayData.LevelType pLevelType)
        {

        }

        private void CleanupListeners()
        {
            learnerLevelController.OnClicked -= OnLearnerLevelClicked;
            movementLevelController.OnClicked -= OnMovementLevelClicked;
            focusLevelController.OnClicked -= OnFocusLevelClicked;
            awarenessLevelController.OnClicked -= OnAwarenessLevelClicked;
            masteryLevelController.OnClicked -= OnMasteryLevelClicked;
        }
    }
}
