using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class GameObjectFactory : MonoBehaviour
    {
        public event Action OnLoadComplete;

        [Header("Prefabs")]
        public List<GameObject> prefabLevels;

        [Header("Canvas Panels")]
        public GameObject mainMenuPanelObject;
        public GameObject levelEnterPanelObject;
        public GameObject levelPlayPanelObject;
        public GameObject levelResultPanelObject;
        public GameObject gameCompletePanelObject;

        [Header("SFX")]
        public GameObject sfxPlayerObject;

        private List<GameObject> levelObjects = new List<GameObject>();

        private GameData gameData;

        public void Init(ref GameData pGameData)
        {
            gameData = pGameData;
        }

        public void Load()
        {
            foreach (GameObject prefab in prefabLevels)
            {
                GameObject levelObject = Instantiate(prefab);
                levelObject.SetActive(false);
                levelObject.transform.SetParent(transform);
                levelObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

                levelObject.GetComponent<LevelMap>().Load(gameData.cellSize);

                levelObjects.Add(levelObject);
            }

            OnLoadComplete?.Invoke();
        }

        public GameObject FetchLevel()
        {
            GameObject levelObject = levelObjects[gameData.levelIndex];
            levelObject.GetComponent<LevelMap>().LoadLevel(gameData.cellSize);

            return levelObject;
        }

        public int GetLastLevelIndex()
        {
            return levelObjects.Count - 1;
        }
    }
}
