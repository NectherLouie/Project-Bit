using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class PlayManager : MonoBehaviour
    {
        private InputSystem inputSystem;
        private LevelSystem levelSystem;
        private MovementSystem movementSystem;
        private TriggerSystem triggerSystem;
        private TimelineSystem timelineSystem;

        private void Awake()
        {
            inputSystem = FindObjectOfType<InputSystem>();
            inputSystem.OnMoveUp += OnMoveInput;
            inputSystem.OnMoveDown += OnMoveInput;
            inputSystem.OnMoveRight += OnMoveInput;
            inputSystem.OnMoveLeft += OnMoveInput;
            inputSystem.OnResetKeyDown += OnResetKeyDown;
            inputSystem.OnTimelineOpened += OnTimelineOpened;
            inputSystem.Init();

            levelSystem = FindObjectOfType<LevelSystem>();
            levelSystem.Init();

            movementSystem = FindObjectOfType<MovementSystem>();
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
            timelineSystem.RecordTimeStamp();
            movementSystem.Move(pVertical, pHorizontal);
        }

        private void OnResetKeyDown()
        {
            SceneManager.LoadScene(0);
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
        }

        private void OnExitActivated(Exit pExit)
        {
            pExit.ToggleSwitchOn();
        }

        private void OnSwitchToggled(Switch pSwitch)
        {
            pSwitch.ToggleSwitchOn();
        }
    }
}
