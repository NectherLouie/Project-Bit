using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bit
{
    public class GameCompletePanel : BasePanel
    {
        public event Action OnMainMenuButtonClicked;

        public SFXPlayer sfxPlayer;

        public void ClickMainMenuButton()
        {
            sfxPlayer.PlaySelectSound();

            Hide();

            OnMainMenuButtonClicked?.Invoke();
        }
    }
}
