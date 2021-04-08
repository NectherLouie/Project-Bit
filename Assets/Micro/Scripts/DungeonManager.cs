using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class DungeonManager : MonoBehaviour
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
            inputSystem.Init();
        }

        private void OnMoveInput(int pVertical, int pHorizontal)
        {
            Debug.Log(pVertical.ToString() + ":" + pHorizontal.ToString());

            if (pHorizontal == 1)
            {
                OnLevelSelected();
            }
        }

        private void OnLevelSelected()
        {
            Debug.Log("OnLevelSelected()");

            inputSystem.OnMoveUp -= OnMoveInput;
            inputSystem.OnMoveDown -= OnMoveInput;
            inputSystem.OnMoveRight -= OnMoveInput;
            inputSystem.OnMoveLeft -= OnMoveInput;
            inputSystem.EnableInput(false);

            transitionSystem.OnFadeComplete += OnLevelSelectTransitionComplete;
            transitionSystem.PlayExitTransition();
        }

        private void OnLevelSelectTransitionComplete()
        {
            transitionSystem.OnFadeComplete -= OnLevelSelectTransitionComplete;

            //gameData.CompleteLevel();
            //gameData.SetLevel();
        }    
    }
}
