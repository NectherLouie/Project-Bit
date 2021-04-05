using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class PlayManager : MonoBehaviour
    {
        public GameData gameData;
        public PlayData playData;

        private TransitionSystem transitionSystem;
        private InputSystem inputSystem;
        private LevelSystem levelSystem;
        private MovementSystem movementSystem;
        private TriggerSystem triggerSystem;
        private TimelineSystem timelineSystem;

        private HUDController hudController;

        private void Awake()
        {
            playData = gameData.GetCurrentPlayData();

            levelSystem = FindObjectOfType<LevelSystem>();
            levelSystem.Init(playData);

            hudController = FindObjectOfType<HUDController>();
            hudController.Init(playData);

            transitionSystem = FindObjectOfType<TransitionSystem>();
            transitionSystem.OnFadeComplete += OnTransitionEnterComplete;
            transitionSystem.Init();
            transitionSystem.PlayEnterTransition();
        }

        private void OnTransitionEnterComplete()
        {
            transitionSystem.OnFadeComplete -= OnTransitionEnterComplete;

            inputSystem = FindObjectOfType<InputSystem>();
            inputSystem.OnMoveUp += OnMoveInput;
            inputSystem.OnMoveDown += OnMoveInput;
            inputSystem.OnMoveRight += OnMoveInput;
            inputSystem.OnMoveLeft += OnMoveInput;
            inputSystem.OnResetKeyDown += OnResetKeyDown;
            inputSystem.OnRewindKeyDown += OnRewindKeyDown;
            inputSystem.Init();

            movementSystem = FindObjectOfType<MovementSystem>();
            movementSystem.OnMoveSucceeded += OnMoveSucceeded;
            movementSystem.OnMoveComplete += OnMoveComplete;
            movementSystem.Init(levelSystem.config);

            triggerSystem = FindObjectOfType<TriggerSystem>();
            triggerSystem.OnExitActivated += OnExitActivated;
            triggerSystem.OnSwitchToggled += OnSwitchToggled;
            triggerSystem.OnCoinMultiplierToggled += OnCoinMultiplierToggled;
            triggerSystem.Init(levelSystem.config);

            timelineSystem = FindObjectOfType<TimelineSystem>();
            timelineSystem.OnTimeStampClicked += OnTimeStampClicked;
            timelineSystem.Init(levelSystem.config);
        }

        private void OnMoveInput(int pVertical, int pHorizontal)
        {
            movementSystem.Move(pVertical, pHorizontal);
        }

        private void OnMoveSucceeded()
        {
            inputSystem.EnableInput(false);

            timelineSystem.RecordTimeStamp();

            gameData.DecreaseMoves();
            gameData.IncreaseCoins(1);

            hudController.UpdateMoves();
            hudController.UpdateCoins();
        }

        private void OnResetKeyDown()
        {
            SceneManager.LoadScene((int)SceneIndices.MAIN_MENU);
        }

        private void OnMoveComplete()
        {
            triggerSystem.TriggerEvents();

            StartCoroutine(Bit.Utils.Wait(0.155f, OnWaitMoveComplete));
        }

        private void OnWaitMoveComplete()
        {
            inputSystem.EnableInput(true);
        }

        private void OnRewindKeyDown()
        {
            TimelineSystem.TimeStampData timeStampData = timelineSystem.GetPreviousTimeStampData();
            
            if (timeStampData != null)
            {
                inputSystem.EnableInput(false);

                levelSystem.LoadTimeStamp(timeStampData);

                triggerSystem.TriggerEvents();

                StartCoroutine(Bit.Utils.Wait(0.155f, OnWaitMoveComplete));
            }
        }

        private void OnTimeStampClicked(TimelineSystem.TimeStampData pTimeStampData)
        {
            levelSystem.LoadTimeStamp(pTimeStampData);
            movementSystem.Move(0, 0);
        }

        private void OnExitActivated(Exit pExit)
        {
            Debug.Log("OnExitActivated()");

            pExit.ToggleSwitchOn();

            inputSystem.OnMoveUp -= OnMoveInput;
            inputSystem.OnMoveDown -= OnMoveInput;
            inputSystem.OnMoveRight -= OnMoveInput;
            inputSystem.OnMoveLeft -= OnMoveInput;
            inputSystem.OnResetKeyDown -= OnResetKeyDown;
            inputSystem.OnRewindKeyDown -= OnRewindKeyDown;
            inputSystem.EnableInput(false);
            StopAllCoroutines();

            transitionSystem.OnFadeComplete += OnExitTransitionComplete;
            transitionSystem.PlayExitTransition();
        }

        private void OnExitTransitionComplete()
        {
            transitionSystem.OnFadeComplete -= OnExitTransitionComplete;

            gameData.CompleteLevel();

            /*
            if (!playData.config.completed)
            {
                SceneManager.LoadScene((int)SceneIndices.PLAY);
            }
            else
            {
                SceneManager.LoadScene((int)SceneIndices.LEVEL_SELECT);
            }
            */
        }

        private void OnSwitchToggled(Switch pSwitch)
        {
            pSwitch.ToggleSwitchOn();
        }

        private void OnCoinMultiplierToggled(CoinMultiplier pCoinMultiplier)
        {
            gameData.IncreaseCoins(-1);
            gameData.IncreaseCoins(1, pCoinMultiplier.multiplier);

            hudController.UpdateCoins();
        }
    }
}
