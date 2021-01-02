using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Micro
{
    public class SelectionLevelController : MonoBehaviour,
            IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action<PlayData.LevelType> OnClicked;

        public PlayData.LevelType levelType = PlayData.LevelType.LEARNER;

        public TMP_Text levelNumbersText;
        public Image lockIcon;
        public Sprite unlockSprite;
        public Color unlockColor;
        public Image gradientBar;

        private bool inputEnabled = false;
        private bool completed = false;

        public void CompleteLevelArea()
        {
            gradientBar.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);

            completed = true;
        }

        public void UnlockLevelArea()
        {
            gradientBar.gameObject.SetActive(false);

            lockIcon.gameObject.SetActive(true);
            lockIcon.sprite = unlockSprite;
            lockIcon.color = unlockColor;
        }

        public void UpdateLevelIndex(int pLevelIndex, int pLevelCount)
        {
            int currentLevel = pLevelIndex + 1;
            levelNumbersText.text = currentLevel.ToString() + "/" + pLevelCount.ToString();
        }

        public void EnableInput(bool pEnabled)
        {
            inputEnabled = pEnabled;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (inputEnabled)
            {
                Debug.Log(name + " Game Object Clicked!");

                OnClicked?.Invoke(levelType);
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (!completed)
            {
                transform.DOScale(1.25f, 0.5f)
                    .SetEase(Ease.OutBack);
            }
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (!completed)
            {
                transform.DOScale(1.0f, 0.5f)
                    .SetEase(Ease.OutBack);
            }
        }
    }
}
