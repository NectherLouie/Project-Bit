using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Micro
{
    public class FadePanelController : MonoBehaviour
    {
        public Action OnFadeComplete;

        public float fadeDuration = 0.5f;

        public void FadeIn()
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();

            cg.alpha = 0;
            cg.DOFade(1.0f, fadeDuration)
                .OnComplete(() => OnFadeComplete?.Invoke());
        }

        public void FadeOut()
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();

            cg.alpha = 1.0f;
            cg.DOFade(0, fadeDuration)
                .OnComplete(() => OnFadeComplete?.Invoke());
        }
    }
}
