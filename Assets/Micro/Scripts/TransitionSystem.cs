using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class TransitionSystem : MonoBehaviour
    {
        public Action OnFadeComplete;
        public Action<Switch> OnSwitchToggled;

        public FadePanelController fadePanelController;

        public void Init()
        {
        }

        public void PlayEnterTransition()
        {
            fadePanelController.OnFadeComplete += OnFadeOutComplete;
            fadePanelController.FadeOut();
        }

        private void OnFadeOutComplete()
        {
            OnFadeComplete?.Invoke();
        }

        public void PlayExitTransition()
        {
            fadePanelController.OnFadeComplete += OnFadeInComplete;
            fadePanelController.FadeIn();
        }

        public void OnFadeInComplete()
        {
            OnFadeComplete?.Invoke();
        }
    }
}
