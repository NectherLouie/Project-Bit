using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class HubManager : MonoBehaviour
    {
        public Action OnLoadComplete;
        public Action OnUnloadComplete;

        public GameData gameData;
        public PlayData playData;
        public HubData hubData;

        private TransitionSystem transitionSystem;
        private InputSystem inputSystem;
        private LevelSystem levelSystem;
        private MovementSystem movementSystem;
        private TriggerSystem triggerSystem;
        private TimelineSystem timelineSystem;

        private HUDController hudController;

        private GameStateManager gameStateManager;

        private void Awake()
        {
            gameStateManager = FindObjectOfType<GameStateManager>();
        }

        public void Load()
        {
            playData = gameData.GetCurrentPlayData();
            hubData = gameData.GetHubData();

            hudController = FindObjectOfType<HUDController>();
            hudController.Init(playData);

            levelSystem = FindObjectOfType<LevelSystem>();
            levelSystem.Init(hubData);

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

            movementSystem = FindObjectOfType<MovementSystem>();
            movementSystem.OnMoveSucceeded += OnMoveSucceeded;
            movementSystem.OnMoveComplete += OnMoveComplete;
            movementSystem.Init(levelSystem.config);

            triggerSystem = FindObjectOfType<TriggerSystem>();
            triggerSystem.OnEntranceActivated += OnEntranceActivated;
            triggerSystem.Init(levelSystem.config);

            OnLoadComplete?.Invoke();
        }

        public void Unload()
        {
            levelSystem.Unload();

            inputSystem.OnMoveUp -= OnMoveInput;
            inputSystem.OnMoveDown -= OnMoveInput;
            inputSystem.OnMoveRight -= OnMoveInput;
            inputSystem.OnMoveLeft -= OnMoveInput;
            inputSystem.Unload();

            StartCoroutine(Bit.Utils.Wait(1.0f, OnUnloaded));
        }

        private void OnUnloaded()
        {
            Debug.Log("HubManager::OnUnloaded()");

            movementSystem.OnMoveSucceeded -= OnMoveSucceeded;
            movementSystem.OnMoveComplete -= OnMoveComplete;
            movementSystem.Unload();

            triggerSystem.OnEntranceActivated -= OnEntranceActivated;
            triggerSystem.Unload();

            OnUnloadComplete?.Invoke();
        }

        private void OnMoveInput(int pVertical, int pHorizontal)
        {
            movementSystem.Move(pVertical, pHorizontal);
        }

        private void OnMoveSucceeded()
        {
            inputSystem.EnableInput(false);
        }

        private void OnMoveComplete()
        {
            StartCoroutine(Bit.Utils.Wait(0.155f, OnWaitMoveComplete));
        }

        private void OnWaitMoveComplete()
        {
            triggerSystem.TriggerEvents();
            inputSystem.EnableInput(true);
        }

        private void OnEntranceActivated(Entrance pEntrance)
        {
            Debug.Log("OnEntranceActivated: " + pEntrance.config.index);

            transitionSystem.OnFadeComplete += OnExitTransitionComplete;
            transitionSystem.PlayExitTransition();
        }

        private void OnExitTransitionComplete()
        {
            transitionSystem.OnFadeComplete -= OnExitTransitionComplete;

            gameStateManager.ChangeToPlay();
        }
    }
}
