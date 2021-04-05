using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameData gameData;

        private MainMenuSystem mainMenuSystem;

        private void Awake()
        {
            gameData.Init();

            mainMenuSystem = FindObjectOfType<MainMenuSystem>();
            mainMenuSystem.OnTitleClicked += OnTitleClicked;
            mainMenuSystem.Init();
        }

        private void OnTitleClicked()
        {
            ChangeScene();
        }

        private void ChangeScene()
        {
            mainMenuSystem.EnableInput(false);

            SceneManager.LoadScene((int)SceneIndices.PLAY);
        }
    }
}
