using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class Game : MonoBehaviour
    {
        public GameData gameData;

        [Header("Services")]
        public GameObjectFactory gameObjectFactory;
        //public Services services;

        [Header("States")]
        public LoadState loadState;
        public MainMenuState mainMenu;
        public LevelEnterState levelEnter;
        public LevelPlayState levelPlay;
        public LevelResultState levelResult;
        public GameCompleteState gameComplete;

        public void Awake()
        {
            // Load game
            gameData.Reset();

            // Init services
            gameObjectFactory.Init(ref gameData);

            // Injections
            loadState.Inject(ref gameData, gameObjectFactory);
            mainMenu.Inject(ref gameData, gameObjectFactory);
            levelEnter.Inject(ref gameData, gameObjectFactory);
            levelPlay.Inject(ref gameData, gameObjectFactory);
            levelResult.Inject(ref gameData, gameObjectFactory);
            gameComplete.Inject(ref gameData, gameObjectFactory);

            // Load
            loadState.OnComplete += LoadComplete;
            loadState.Enter();
        }

        private void LoadComplete()
        {
            loadState.OnComplete -= LoadComplete;

            mainMenu.OnComplete += MainMenuComplete;
            mainMenu.Enter();
        }

        private void MainMenuComplete()
        {
            mainMenu.OnComplete -= MainMenuComplete;

            levelEnter.OnComplete += EnterComplete;
            levelEnter.Enter();
        }

        // --- State Loop ---------------------------------------
        private void EnterComplete()
        {
            levelEnter.OnComplete -= EnterComplete;

            levelPlay.OnComplete += PlayComplete;
            levelPlay.OnRestartEvent += RestartLevel;
            levelPlay.Enter();
        }

        private void RestartLevel()
        {
            levelPlay.OnComplete -= PlayComplete;
            levelPlay.OnRestartEvent -= RestartLevel;

            levelEnter.OnComplete += EnterComplete;
            levelEnter.Enter();
        }

        private void PlayComplete()
        {
            levelPlay.OnComplete -= PlayComplete;
            levelPlay.OnRestartEvent -= RestartLevel;

            levelResult.OnComplete += ResultComplete;
            levelResult.Enter();
        }

        private void ResultComplete()
        {
            levelResult.OnComplete -= ResultComplete;

            // If last level 
            if (gameData.levelIndex >= gameData.lastLevelIndex + 1)
            {
                gameComplete.OnComplete += GameComplete;
                gameComplete.Enter();
            }
            else
            {
                levelEnter.OnComplete += EnterComplete;
                levelEnter.Enter();
            }
        }

        // --- Game Complete
        private void GameComplete()
        {
            gameComplete.OnComplete -= LoadComplete;

            mainMenu.OnComplete += MainMenuComplete;
            mainMenu.Enter();
        }
    }
}
