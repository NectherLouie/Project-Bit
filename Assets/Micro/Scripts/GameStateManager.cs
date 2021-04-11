using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Micro
{
    public class GameStateManager : MonoBehaviour
    {
        public HubManager hubManager;
        public PlayManager playManager;

        private void Awake()
        {
            playManager = FindObjectOfType<PlayManager>();
            hubManager = FindObjectOfType<HubManager>();

            hubManager.Load();
        }

        public void ChangeToPlay()
        {
            hubManager.OnUnloadComplete += OnHubUnloaded;
            hubManager.Unload();
        }

        private void OnHubUnloaded()
        {
            hubManager.OnUnloadComplete -= OnHubUnloaded;
            
            playManager.Load();
        }

        public void ChangeToHub()
        {
            playManager.OnUnloadComplete += OnPlayUnloaded;
            playManager.Unload();
        }

        private void OnPlayUnloaded()
        {
            playManager.OnUnloadComplete -= OnPlayUnloaded;

            hubManager.Load();
        }
    }
}
