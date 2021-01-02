using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Micro
{
    public class HUDController : MonoBehaviour
    {
        public TMP_Text movesText;
        public TMP_Text zenText;
        public TMP_Text levelsText;

        private PlayData playData;

        public void Init(PlayData pPlayData)
        {
            playData = pPlayData;

            UpdateMoves();
            UpdateZenCount();
            UpdateLevelsText();
        }

        public void UpdateMoves()
        {
            movesText.text = playData.config.moveCount.ToString();
        }

        public void UpdateZenCount()
        {
            zenText.text = playData.config.zenModeCount.ToString();
        }

        public void UpdateLevelsText()
        {
            int currentLevel = playData.config.levelIndex + 1;
            levelsText.text = currentLevel.ToString() + "/" + playData.config.levelCount.ToString();
        }
    }
}
