using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class GameCompleteState : BaseState
    {
        public event Action OnComplete;

        private GameCompletePanel gameCompletePanel;

        public void Enter()
        {
            gameCompletePanel = gameObjectFactory.gameCompletePanelObject.GetComponent<GameCompletePanel>();

            gameCompletePanel.OnMainMenuButtonClicked += MainMenuButtonClicked;
            gameCompletePanel.Show();
        }

        private void MainMenuButtonClicked()
        {
            gameCompletePanel.OnMainMenuButtonClicked -= MainMenuButtonClicked;

            gameData.levelIndex = 0;

            OnComplete?.Invoke();
        }
    }
}
