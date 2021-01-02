using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class LevelSelectManager : MonoBehaviour
    {
        public GameData gameData;

        private LevelSelectSystem levelSelectSystem;

        private void Awake()
        {
            levelSelectSystem = FindObjectOfType<LevelSelectSystem>();
            levelSelectSystem.OnLevelSelectClicked += OnLevelSelectClicked;
            levelSelectSystem.Init(gameData);
        }

        private void OnLevelSelectClicked(PlayData.LevelType pLevelType)
        {
            SceneManager.LoadScene((int)SceneIndices.PLAY);
        }
    }
}
