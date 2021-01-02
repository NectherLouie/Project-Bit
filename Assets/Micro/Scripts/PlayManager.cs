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
            inputSystem.OnTimelineOpened += OnTimelineOpened;
            inputSystem.Init();

            movementSystem = FindObjectOfType<MovementSystem>();
            movementSystem.OnMoveSucceeded += OnMoveSucceeded;
            movementSystem.OnMoveComplete += OnMoveComplete;
            movementSystem.Init(levelSystem.config);

            triggerSystem = FindObjectOfType<TriggerSystem>();
            triggerSystem.OnExitActivated += OnExitActivated;
            triggerSystem.OnSwitchToggled += OnSwitchToggled;
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
            timelineSystem.RecordTimeStamp();

            gameData.DecreaseMoves();

            hudController.UpdateMoves();
        }

        private void OnResetKeyDown()
        {
            SceneManager.LoadScene((int)SceneIndices.MAIN_MENU);
        }

        private void OnMoveComplete()
        {
            triggerSystem.TriggerEvents();
        }

        private void OnTimelineOpened()
        {
            timelineSystem.Open();
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

            transitionSystem.OnFadeComplete += OnExitTransitionComplete;
            transitionSystem.PlayExitTransition();
        }

        private void OnExitTransitionComplete()
        {
            transitionSystem.OnFadeComplete -= OnExitTransitionComplete;

            gameData.CompleteLevel();

            if (!playData.config.completed)
            {
                SceneManager.LoadScene((int)SceneIndices.PLAY);
            }
            else
            {
                SceneManager.LoadScene((int)SceneIndices.LEVEL_SELECT);
            }
        }

        private void OnSwitchToggled(Switch pSwitch)
        {
            pSwitch.ToggleSwitchOn();
        }
    }
}
