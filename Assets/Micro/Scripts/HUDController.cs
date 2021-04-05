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
        public TMP_Text coinsText;

        private PlayData playData;

        public void Init(PlayData pPlayData)
        {
            playData = pPlayData;

            UpdateMoves();
            UpdateCoins();
        }

        public void UpdateMoves()
        {
            movesText.text = playData.moveCount.ToString();
        }

        public void UpdateCoins()
        {
            coinsText.text = playData.coinCount.ToString();
        }
    }
}
