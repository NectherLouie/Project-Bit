using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class LevelEnterState : BaseState
    {
        public event Action OnComplete;

        private LevelEnterPanel levelEnterPanel;

        private SFXPlayer sfxPlayer;

        public void Enter()
        {
            sfxPlayer = gameObjectFactory.sfxPlayerObject.GetComponent<SFXPlayer>();

            levelEnterPanel = gameObjectFactory.levelEnterPanelObject.GetComponent<LevelEnterPanel>();

            levelEnterPanel.OnShowComplete += EnterComplete;
            levelEnterPanel.SetLevelNumber(gameData.levelIndex + 1);
            levelEnterPanel.Show();
        }

        private void EnterComplete()
        {
            //sfxPlayer.PlayLevelStartSound();

            levelEnterPanel.OnShowComplete -= EnterComplete;
            levelEnterPanel.Hide(0.25f, 1f);

            OnComplete?.Invoke();
        }
    }
}
