using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class LevelResultState : BaseState
    {
        public event Action OnComplete;

        private LevelResultPanel levelResultPanel;

        public void Enter()
        {
            levelResultPanel = gameObjectFactory.levelResultPanelObject.GetComponent<LevelResultPanel>();

            
            levelResultPanel.OnShowComplete += LevelResultPanelShown;
            levelResultPanel.Show();
        }

        private void LevelResultPanelShown()
        {
            levelResultPanel.OnShowComplete -= LevelResultPanelShown;
            levelResultPanel.OnNextButtonClicked += NextButtonClicked;
            levelResultPanel.StartCountDown();
        }

        private void NextButtonClicked()
        {
            levelResultPanel.OnNextButtonClicked -= NextButtonClicked;

            gameData.levelIndex++;

            OnComplete?.Invoke();
        }
    }
}
