using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [Serializable]
    public class PlayData
    {
        public int levelIndex = 0;
        public int moveCount;
        public int coinCount;

        public List<GameObject> levelPrefabs = new List<GameObject>();

        public void Load()
        {

        }

        public void Reset()
        {
            levelIndex = 0;
            moveCount = 0;
            coinCount = 0;
        }
    }

    [Serializable]
    public class HubData: PlayData
    {

    }

    [CreateAssetMenu(fileName = "New Micro Game Data", menuName = "Micro Game Data")]
    public class GameData : ScriptableObject
    {
        public HubData hubData = new HubData();
        public PlayData playData = new PlayData();

        public bool gameComplete = false;

        public void Init()
        {
            gameComplete = false;

            playData.Reset();
        }

        public HubData GetHubData()
        {
            return hubData;
        }

        public PlayData GetCurrentPlayData()
        {
            return playData;
        }

        public void DecreaseMoves()
        {
            playData.moveCount--;
        }

        public void IncreaseCoins(int pValue)
        {
            playData.coinCount += pValue;
        }

        public void IncreaseCoins(int pValue, int pMultiplier)
        {
            playData.coinCount += pValue * pMultiplier;
        }

        public void CompleteLevel()
        {

        }
    }
}
