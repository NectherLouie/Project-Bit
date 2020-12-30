using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Micro
{
    public class TimelineSystem : MonoBehaviour
    {
        public Action<TimeStampData> OnTimeStampClicked;

        [Serializable]
        public class TimeStampData
        {
            public List<Player.Config> players = new List<Player.Config>();
            public List<Box.Config> boxes = new List<Box.Config>();
            public List<Wall.Config> walls = new List<Wall.Config>();
            public List<Exit.Config> exits = new List<Exit.Config>();
            public List<Gate.Config> gates = new List<Gate.Config>();
            public List<Switch.Config> switches = new List<Switch.Config>();

            public Texture2D snapshotTexture;
            public Sprite snapshotSprite;
        }

        [Serializable]
        public class Config
        {
            public List<Player> players = new List<Player>();
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Exit> exits = new List<Exit>();
            public List<Gate> gates = new List<Gate>();
            public List<Switch> switches = new List<Switch>();

            public List<TimeStampData> timeStampData = new List<TimeStampData>();

            public void ResetGameObjects()
            {
                players.Clear();
                boxes.Clear();
                walls.Clear();
                exits.Clear();
                gates.Clear();
                switches.Clear();

                timeStampData.Clear();
            }
        }

        public Config config = new Config();

        public Camera camera;
        public GameObject prefabImage;
        public Transform contentDisplay;
        public GameObject scrollView;

        private bool scrollViewOpen = false;
        private List<GameObject> scrollViewContent = new List<GameObject>();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();

            config.players = pConfig.players;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
            config.exits = pConfig.exits;
            config.gates = pConfig.gates;
            config.switches = pConfig.switches;
        }

        public void Open()
        {
            scrollViewOpen = !scrollViewOpen;
            scrollView.SetActive(scrollViewOpen);

            foreach (GameObject g in scrollViewContent)
            {
                Destroy(g);
            }

            scrollViewContent.Clear();

            // choose 3 or less from stamp data
            int timeStampCount = config.timeStampData.Count;
            RectTransform contentRect = contentDisplay.gameObject.GetComponent<RectTransform>();
            Vector2 sizeD = contentRect.sizeDelta;
            sizeD.x = 560.0f * timeStampCount;
            contentRect.sizeDelta = sizeD;

            for (int i = 0; i < timeStampCount; ++i)
            {
                GameObject g = Instantiate(prefabImage);
                g.transform.SetParent(contentDisplay);

                TimeStampData timeStampData = config.timeStampData[i];

                RectTransform rectTransform = g.GetComponent<RectTransform>();

                Vector2 anchoredPos = rectTransform.anchoredPosition;
                anchoredPos.x = (500.0f * i) + (50.0f * (i + 1));
                anchoredPos.y = 0;
                rectTransform.anchoredPosition = anchoredPos;

                Image image = g.GetComponent<Image>();
                image.sprite = timeStampData.snapshotSprite;

                Button button = g.GetComponent<Button>();
                button.onClick.AddListener(() => ClickTimeStamp(timeStampData));

                scrollViewContent.Add(g);
            }
        }

        private void ClickTimeStamp(TimeStampData pTimeStampData)
        {
            scrollViewOpen = !scrollViewOpen;
            scrollView.SetActive(scrollViewOpen);

            foreach (GameObject g in scrollViewContent)
            {
                Image image = g.GetComponent<Image>();
                image.sprite = null;

                Button button = g.GetComponent<Button>();
                button.onClick.RemoveAllListeners();

                Destroy(g);
            }

            scrollViewContent.Clear();

            OnTimeStampClicked?.Invoke(pTimeStampData);
        }

        public void RecordTimeStamp()
        {
            TimeStampData timeStampData = new TimeStampData();
            timeStampData.players = CloneConfigs(config.players);
            timeStampData.boxes = CloneConfigs(config.boxes);
            timeStampData.walls = CloneConfigs(config.walls);
            timeStampData.exits = CloneConfigs(config.exits);
            timeStampData.gates = CloneConfigs(config.gates);
            timeStampData.switches = CloneConfigs(config.switches);

            // Clean up textures for leaks
            Destroy(timeStampData.snapshotTexture);
            Destroy(timeStampData.snapshotSprite);

            timeStampData.snapshotTexture = GenerateSnapShotTexture();
            timeStampData.snapshotSprite = GenerateSnapShotSprite(timeStampData.snapshotTexture);

            if (config.timeStampData.Count >= 10)
            {
                TimeStampData tsd = Bit.Utils.Slice(config.timeStampData, 0);

                Destroy(tsd.snapshotTexture);
                Destroy(tsd.snapshotSprite);

                config.timeStampData.RemoveAt(0);
            }

            config.timeStampData.Add(timeStampData);
        }

        public Sprite GenerateSnapShotSprite(Texture2D pSnapShotTexture)
        {
            return Sprite.Create(pSnapShotTexture, new Rect(0, 0, pSnapShotTexture.width, pSnapShotTexture.height), new Vector2(0.5f, 0.5f));
        }

        public Texture2D GenerateSnapShotTexture()
        {
            int width = 1920;
            int height = 1080;

            RenderTexture.active = camera.activeTexture;

            Texture2D snapshotTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
            snapshotTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            snapshotTexture.Apply();

            RenderTexture.active = null;

            return snapshotTexture;
        }

        private List<Movable.Config> CloneConfigs<T>(List<T> pList) where T: Movable
        {
            // Extract
            List<Movable.Config> extractedConfigs = new List<Movable.Config>();

            foreach (T box in pList)
            {
                extractedConfigs.Add(box.config);
            }

            // Clone
            List<Movable.Config> clonedConfigs = new List<Movable.Config>();

            foreach (Movable.Config p in extractedConfigs)
            {
                clonedConfigs.Add(p.Clone());
            }

            return clonedConfigs;
        }
    }
}
