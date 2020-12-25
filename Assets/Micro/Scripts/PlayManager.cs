using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            inputSystem.Init();

            levelSystem = FindObjectOfType<LevelSystem>();
            levelSystem.Init();

            movementSystem = FindObjectOfType<MovementSystem>();
            movementSystem.OnMoveComplete += OnMoveComplete;
            movementSystem.Init(levelSystem.config);

            triggerSystem = FindObjectOfType<TriggerSystem>();
            triggerSystem.Init(levelSystem.config);

            timelineSystem = FindObjectOfType<TimelineSystem>();
            timelineSystem.Init(levelSystem.config);
        }

        private void OnMoveInput(int pVertical, int pHorizontal)
        {
            movementSystem.Move(pVertical, pHorizontal);
        }

        private void OnMoveComplete()
        {
            triggerSystem.TriggerEvents();
            timelineSystem.RecordTimeStamp();
        }
    }
}
