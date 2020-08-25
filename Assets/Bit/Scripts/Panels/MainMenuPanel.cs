using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bit
{
    public class MainMenuPanel : BasePanel
    {
        public event Action OnPlayButtonClicked;

        public SFXPlayer sfxPlayer;

        public void ClickPlayButton()
        {
            sfxPlayer.PlaySelectSound();

            Hide();

            OnPlayButtonClicked?.Invoke();
        }

        public void ClickQuitButton()
        {
            sfxPlayer.PlaySelectSound();

            Application.Quit();
        }
    }
}
