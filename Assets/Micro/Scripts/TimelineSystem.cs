using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class TimelineSystem : MonoBehaviour
    {
        [Serializable]
        public class TimeStampData
        {
            public List<Player.Config> players = new List<Player.Config>();
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Gate> gates = new List<Gate>();
            public List<Switch> switches = new List<Switch>();

            public RenderTexture renderTexture = new RenderTexture(1920, 1080, 16);
        }

        [Serializable]
        public class Config
        {
            public List<Player> players;
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Gate> gates = new List<Gate>();
            public List<Switch> switches = new List<Switch>();

            public List<TimeStampData> timeStampData = new List<TimeStampData>();

            public void ResetGameObjects()
            {
                players.Clear();
                boxes.Clear();
                walls.Clear();
                gates.Clear();
                switches.Clear();

                timeStampData.Clear();
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();

            config.players = pConfig.players;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
            config.gates = pConfig.gates;
            config.switches = pConfig.switches;
        }

        public void RecordTimeStamp()
        {
            TimeStampData timeStampData = new TimeStampData();
            timeStampData.players = ClonePlayerConfigs(ExtractPlayerConfigList(config.players));
            timeStampData.boxes = config.boxes;
            timeStampData.walls = config.walls;
            timeStampData.gates = config.gates;
            timeStampData.switches = config.switches;

            config.timeStampData.Add(timeStampData);

            foreach(TimeStampData tsd in config.timeStampData)
            {
                Debug.Log(tsd.players[0].posX);
            }
            
            Debug.Log("\n");
        }

        private List<Player.Config> ExtractPlayerConfigList(List<Player> pList)
        {
            List<Player.Config> output = new List<Player.Config>();

            foreach(Player player in pList)
            {
                output.Add(player.config);
            }

            return output;
        }

        private List<Player.Config> ClonePlayerConfigs(List<Player.Config> pList)
        {
            List<Player.Config> output = new List<Player.Config>();

            foreach (Player.Config p in pList)
            {
                output.Add(p.Clone());
            }

            return output;
        }
    }
}
