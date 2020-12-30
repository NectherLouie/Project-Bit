using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class MainMenuManager : MonoBehaviour
    {
        private MainMenuSystem mainMenuSystem;

        private void Awake()
        {
            mainMenuSystem = FindObjectOfType<MainMenuSystem>();
            mainMenuSystem.OnTitleClicked += OnTitleClicked;
            mainMenuSystem.Init();
        }

        private void OnTitleClicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}
