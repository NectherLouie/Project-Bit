using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

            // ---
            PlayData levelLearnerData = gameData.playDataList[0];
            learnerLevelController.OnClicked += OnSelectionLevelClicked;
            learnerLevelController.EnableInput(false);
            
            if (levelLearnerData.config.unlocked)
            {
                learnerLevelController.UpdateLevelIndex(levelLearnerData.config.levelIndex, levelLearnerData.config.levelCount);
                learnerLevelController.UnlockLevelArea();
                learnerLevelController.EnableInput(true);

                if (levelLearnerData.config.completed)
                {
                    learnerLevelController.CompleteLevelArea();
                    learnerLevelController.EnableInput(false);
                }
            }

            // ---
            PlayData levelMovementData = gameData.playDataList[1];
            movementLevelController.OnClicked += OnSelectionLevelClicked;
            movementLevelController.EnableInput(false);
            
            if (levelMovementData.config.unlocked)
            {
                movementLevelController.UpdateLevelIndex(levelMovementData.config.levelIndex, levelMovementData.config.levelCount);
                movementLevelController.UnlockLevelArea();
                movementLevelController.EnableInput(true);

                if (levelMovementData.config.completed)
                {
                    movementLevelController.CompleteLevelArea();
                    movementLevelController.EnableInput(false);
                }
            }

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

        private void OnSelectionLevelClicked(PlayData.LevelType pLevelType)
        {
            fadePanelController.OnFadeComplete += OnFadeInComplete;
            fadePanelController.FadeIn();
        }

        private void OnFadeInComplete()
        {
            CleanupListeners();

            fadePanelController.OnFadeComplete -= OnFadeInComplete;

            DOTween.KillAll();

            OnLevelSelectClicked?.Invoke(levelType);
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
            learnerLevelController.OnClicked -= OnSelectionLevelClicked;
            movementLevelController.OnClicked -= OnSelectionLevelClicked;
            focusLevelController.OnClicked -= OnFocusLevelClicked;
            awarenessLevelController.OnClicked -= OnAwarenessLevelClicked;
            masteryLevelController.OnClicked -= OnMasteryLevelClicked;
        }
    }
}
