using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [Serializable]
    public class PlayData
    {
        public enum LevelType
        {
            LEARNER,
            MOVEMENT,
            FOCUS,
            AWARENESS,
            MASTERY
        }
        public LevelType levelType;
        public bool unlocked;
        public bool completed;
        public int levelIndex;
        public int levelCount;
        public int moveCount;
        public int zenModeCount;

        public List<GameObject> levelPrefabs = new List<GameObject>();

        public void Reset()
        {
            levelType = LevelType.LEARNER;
            unlocked = false;
            completed = false;
            levelIndex = 0;
            levelCount = 0;
            moveCount = 0;
            zenModeCount = 0;
        }
    }

    [CreateAssetMenu(fileName = "New Micro Game Data", menuName = "Micro Game Data")]
    public class GameData : ScriptableObject
    {
        public PlayData levelLearnerData = new PlayData();
        public PlayData levelMovementData = new PlayData();
        public PlayData levelFocusData = new PlayData();
        public PlayData levelAwarenessData = new PlayData();
        public PlayData levelMasteryData = new PlayData();

        public PlayData.LevelType currentLevelType = PlayData.LevelType.LEARNER;

        public List<PlayData> playDataList = new List<PlayData>();

        public void Init()
        {
            playDataList.Clear();
            playDataList.Add(levelLearnerData);
            playDataList.Add(levelMovementData);
            playDataList.Add(levelFocusData);
            playDataList.Add(levelAwarenessData);
            playDataList.Add(levelMasteryData);
        }

        public void Reset()
        {

        }

        public PlayData GetCurrentPlayData()
        {
            PlayData output = null;

            foreach(PlayData p in playDataList)
            {
                if (p.levelType == currentLevelType)
                {
                    output = p;
                    break;
                }
            }

            return output;
        }
    }
}
