using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class SFXPlayer : MonoBehaviour
    {
        public AudioSource sfxMove;
        public AudioSource sfxBox;
        public AudioSource sfxKey;
        public AudioSource sfxSelect;
        public AudioSource sfxReset;
        public AudioSource sfxLevelStart;

        public void PlayMoveSound()
        {
            sfxMove.Play();
        }

        public void PlayBoxSound()
        {
            sfxBox.Play();
        }

        public void PlayKeySound()
        {
            sfxKey.Play();
        }

        public void PlaySelectSound()
        {
            sfxSelect.Play();
        }

        public void PlayResetSound()
        {
            sfxReset.Play();
        }

        public void PlayLevelStartSound()
        {
            sfxLevelStart.Play();
        }
    }
}
