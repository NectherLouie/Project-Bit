using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class MainMenuState : BaseState
    {
        public event Action OnComplete;

        private MainMenuPanel mainMenuPanel;

        public void Enter()
        {
            mainMenuPanel = gameObjectFactory.mainMenuPanelObject.GetComponent<MainMenuPanel>();

            mainMenuPanel.OnPlayButtonClicked += PlayButtonClicked;
            mainMenuPanel.Show();
        }

        private void PlayButtonClicked()
        {
            mainMenuPanel.OnPlayButtonClicked -= PlayButtonClicked;
            
            OnComplete?.Invoke();
        }
    }
}
