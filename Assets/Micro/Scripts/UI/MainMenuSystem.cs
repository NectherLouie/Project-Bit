using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class MainMenuSystem : MonoBehaviour
    {
        public Action OnTitleClicked;

        public MainMenuPanelController titlePanelController;
        public FadePanelController fadePanelController;

        public void Init()
        {
            titlePanelController.OnPanelClicked += OnPanelClicked;
            titlePanelController.EnableInput(false);

            fadePanelController.OnFadeComplete += OnFadeOutComplete;
            fadePanelController.FadeOut();
        }

        private void OnPanelClicked()
        {
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
