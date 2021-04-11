using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class LevelSystem : MonoBehaviour
    {
        [Serializable]
        public class Config
        {
            public Transform levelTransform;

            [HideInInspector]
            public List<Player> players = new List<Player>();

            [HideInInspector]
            public List<Box> boxes = new List<Box>();

            [HideInInspector]
            public List<Wall> walls = new List<Wall>();

            [HideInInspector]
            public List<Exit> exits = new List<Exit>();

            [HideInInspector]
            public List<Switch> switches = new List<Switch>();

            [HideInInspector]
            public List<Gate> gates = new List<Gate>();

            [HideInInspector]
            public List<CoinMultiplier> coinMultipliers = new List<CoinMultiplier>();

            [HideInInspector]
            public List<Entrance> entrances = new List<Entrance>();
        }

        public Config config = new Config();

        private PlayData playData;

        private GameObject levelObject;

        public void Init(PlayData pPlayData)
        {
            playData = pPlayData;

            // Instantiate level prefab using level data
            levelObject = Instantiate(playData.levelPrefabs[playData.levelIndex]);
            
            // Set levelTransform to the instansiated prefab
            config.levelTransform = levelObject.transform;

            // Populate level
            int playerCount = 0;
            int boxCount = 0;
            int wallCount = 0;
            int gateCount = 0;
            int switchCount = 0;
            int goalIndex = 0;
            int coinMultiplierCount = 0;
            int entranceCount = 0;

            foreach (Transform t in config.levelTransform)
            {
                Vector3 pos = t.localPosition;

                if (t.GetComponent<Player>() != null)
                {
                    Player player = t.GetComponent<Player>();
                    player.Load(playerCount, pos);

                    config.players.Add(player);

                    playerCount++;
                }

                if (t.GetComponent<Box>() != null)
                {
                    Box box = t.GetComponent<Box>();
                    box.Load(boxCount, pos);

                    config.boxes.Add(box);

                    boxCount++;
                }

                if (t.GetComponent<Wall>() != null)
                {
                    Wall wall = t.GetComponent<Wall>();
                    wall.Load(wallCount, pos);

                    config.walls.Add(wall);

                    wallCount++;
                }

                if (t.GetComponent<Exit>() != null)
                {
                    Exit exit = t.GetComponent<Exit>();
                    exit.Load(goalIndex, pos);

                    config.exits.Add(exit);

                    goalIndex++;
                }

                if (t.GetComponent<Gate>() != null)
                {
                    Gate gate = t.GetComponent<Gate>();
                    gate.Load(gateCount, pos);

                    config.gates.Add(gate);

                    gateCount++;
                }

                if (t.GetComponent<Switch>() != null)
                {
                    Switch sw = t.GetComponent<Switch>();
                    sw.Load(switchCount, pos);

                    config.switches.Add(sw);

                    switchCount++;
                }

                if (t.GetComponent<CoinMultiplier>() != null)
                {
                    CoinMultiplier cm = t.GetComponent<CoinMultiplier>();
                    cm.Load(coinMultiplierCount, pos);

                    config.coinMultipliers.Add(cm);

                    coinMultiplierCount++;
                }

                if (t.GetComponent<Entrance>() != null)
                {
                    Entrance ent = t.GetComponent<Entrance>();
                    ent.Load(entranceCount, pos);

                    config.entrances.Add(ent);

                    entranceCount++;
                }
            }
        }

        public void Unload()
        {
            Destroy(levelObject);
            levelObject = null;
        }

        public void LoadTimeStamp(TimelineSystem.TimeStampData pTimeStampData)
        {
            // Players
            foreach (Movable.Config playerConfig in pTimeStampData.players)
            {
                Player player = config.players[playerConfig.index];
                player.LoadTimeStamp(new Vector2(playerConfig.posX, playerConfig.posY));
            }

            // Boxes
            foreach (Movable.Config boxConfig in pTimeStampData.boxes)
            {
                Box box = config.boxes[boxConfig.index];
                box.LoadTimeStamp(new Vector2(boxConfig.posX, boxConfig.posY));
            }
        }
    }
}
