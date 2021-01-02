using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [Serializable]
    public class PlayData
    {
        [Serializable]
        public enum LevelType
        {
            LEARNER,
            MOVEMENT,
            FOCUS,
            AWARENESS,
            MASTERY
        }

        [Serializable]
        public class Config
        {
            public LevelType levelType;
            public bool unlocked;
            public bool completed;
            public int levelIndex;
            public int levelCount;
            public int moveCount;
            public int zenModeCount;
        }
        
        public List<GameObject> levelPrefabs = new List<GameObject>();

        public Config config = new Config();


        public void Reset()
        {
            config.levelType = LevelType.LEARNER;
            config.unlocked = false;
            config.completed = false;
            config.levelIndex = 0;
            config.levelCount = 0;
            config.moveCount = 0;
            config.zenModeCount = 0;
        }

        public void Load(Config pConfig)
        {
            Reset();
            config = pConfig;
        }

        public Config CloneConfig()
        {
            Config output = new Config();

            output.levelType = config.levelType;
            output.unlocked = config.unlocked;
            output.completed = config.completed;
            output.levelIndex = config.levelIndex;
            output.levelCount = config.levelCount;
            output.moveCount = config.moveCount;
            output.zenModeCount = config.zenModeCount;

            return output;
        }
    }

    [CreateAssetMenu(fileName = "New Micro Game Data", menuName = "Micro Game Data")]
    public class GameData : ScriptableObject
    {
        public PlayData levelLearnerData = new PlayData();
        public PlayData levelMovementData = new PlayData();
        private PlayData levelFocusData = new PlayData();
        private PlayData levelAwarenessData = new PlayData();
        private PlayData levelMasteryData = new PlayData();

        public PlayData.LevelType currentLevelType = PlayData.LevelType.LEARNER;
        public PlayData currentPlayData;
        public PlayData previousPlayData;

        public List<PlayData> playDataList = new List<PlayData>();
        public int playDataIndex = 0;

        public bool gameComplete = false;

        public void Init()
        {
            playDataIndex = 0;
            gameComplete = false;

            // Init all data
            PlayData.Config learnerConfig = new PlayData.Config();
            learnerConfig.levelType = PlayData.LevelType.LEARNER;
            learnerConfig.unlocked = true;
            learnerConfig.completed = false;
            learnerConfig.levelIndex = 0;
            learnerConfig.levelCount = 5;
            learnerConfig.moveCount = 100;
            learnerConfig.zenModeCount = 1;
            levelLearnerData.Load(learnerConfig);

            PlayData.Config movementConfig = new PlayData.Config();
            movementConfig.levelType = PlayData.LevelType.MOVEMENT;
            movementConfig.unlocked = false;
            movementConfig .completed = false;
            movementConfig .levelIndex = 0;
            movementConfig .levelCount = 10;
            movementConfig .moveCount = 100;
            movementConfig.zenModeCount = 1;
            levelMovementData.Load(movementConfig);

            // Add all data to list
            playDataList.Clear();
            playDataList.Add(levelLearnerData);
            playDataList.Add(levelMovementData);
            playDataList.Add(levelFocusData);
            playDataList.Add(levelAwarenessData);
            playDataList.Add(levelMasteryData);
        }

        public PlayData GetCurrentPlayData()
        {
            currentPlayData = playDataList[playDataIndex];
            currentLevelType = currentPlayData.config.levelType;

            return currentPlayData;
        }

        public void DecreaseMoves()
        {
            currentPlayData.config.moveCount--;
        }

        public void CompleteLevel()
        {
            currentPlayData.config.levelIndex++;

            if (currentPlayData.config.levelIndex > currentPlayData.config.levelCount - 1)
            {
                currentPlayData.config.levelIndex = currentPlayData.config.levelCount - 1;
                currentPlayData.config.completed = true;

                previousPlayData = currentPlayData;
                previousPlayData.config = currentPlayData.CloneConfig();

                playDataIndex++;
                if (playDataIndex > playDataList.Count - 1)
                {
                    playDataIndex = playDataList.Count - 1;
                    gameComplete = true;
                }

                if (!gameComplete)
                {
                    currentPlayData = GetCurrentPlayData();
                    currentPlayData.config.unlocked = true;
                    currentPlayData.config.moveCount += previousPlayData.config.moveCount;
                    currentPlayData.config.zenModeCount += previousPlayData.config.zenModeCount;
                }
            }
        }
    }
}
