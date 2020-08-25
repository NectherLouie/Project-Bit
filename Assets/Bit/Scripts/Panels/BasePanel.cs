using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bit
{
    public class BasePanel : MonoBehaviour
    {
        public event Action OnShowComplete;
        public event Action OnHideComplete;

        public void Show(float duration = 0.25f, float delay = 0)
        {
            gameObject.SetActive(true);

            CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            canvasGroup.DOFade(1f, duration)
                .SetDelay(delay)
                .OnComplete(ShowComplete);
        }

        private void ShowComplete()
        {
            OnShowComplete?.Invoke();
        }

        public void Hide(float duration = 0.25f, float delay = 0)
        {
            CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;

            canvasGroup.DOFade(0f, duration)
                .SetDelay(delay)
                .OnComplete(HideComplete);
        }

        private void HideComplete()
        {
            gameObject.SetActive(false);

            OnHideComplete?.Invoke();
        }
    }
}
