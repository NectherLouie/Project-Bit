using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class MainMenuSystem : MonoBehaviour
    {
        public Action OnTitleClicked;
        public Action OnSpaceKeyDown;
        public Action OnEnterKeyDown;

        public MainMenuPanelController titlePanelController;
        public FadePanelController fadePanelController;

        private bool inputEnabled = false;

        public void Init()
        {
            EnableInput(true);

            titlePanelController.OnPanelClicked += OnPanelClicked;
            titlePanelController.EnableInput(false);

            fadePanelController.OnFadeComplete += OnFadeOutComplete;
            fadePanelController.FadeOut();
        }

        public void EnableInput(bool pEnabled)
        {
            inputEnabled = pEnabled;
        }

        private void Update()
        {
            if (!inputEnabled)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                OnPanelClicked();
            }
        }

        private void OnPanelClicked()
        {
            titlePanelController.OnPanelClicked -= OnPanelClicked;
            titlePanelController.EnableInput(false);

            fadePanelController.OnFadeComplete += OnFadeInComplete;
            fadePanelController.FadeIn();
        }

        private void OnFadeInComplete()
        {
            fadePanelController.OnFadeComplete -= OnFadeInComplete;
            OnTitleClicked?.Invoke();
        }

        private void OnFadeOutComplete()
        {
            fadePanelController.OnFadeComplete -= OnFadeOutComplete;
            titlePanelController.EnableInput(true);
        }
    }
}
