using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Security.Policy;

namespace Bit
{
    public class LevelResultPanel : BasePanel
    {
        public event Action OnNextButtonClicked;

        public TMP_Text countText;

        public int currentCount = 3;
        public int countStart = 3;
        public int countEnd = 1;

        public void StartCountDown()
        {
            currentCount = countStart;
            countText.text = currentCount.ToString();
            StartCoroutine(Utils.Wait(1f, OnWaitComplete));
        }

        private void OnWaitComplete()
        {
            if (currentCount > countEnd)
            {
                currentCount--;

                countText.text = currentCount.ToString();

                StartCoroutine(Utils.Wait(1f, OnWaitComplete));
            }
            else
            {
                ClickNextButton();
            }
        }

        public void ClickNextButton()
        {
            Hide();

            OnNextButtonClicked?.Invoke();
        }
    }
}
