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
            public Player.Config player;
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Gate> gates = new List<Gate>();
            public List<Switch> switches = new List<Switch>();
        }

        [Serializable]
        public class Config
        {
            public Player player;
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Gate> gates = new List<Gate>();
            public List<Switch> switches = new List<Switch>();

            public List<TimeStampData> timeStampData = new List<TimeStampData>();

            public void ResetGameObjects()
            {
                player = null;
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

            config.player = pConfig.player;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
            config.gates = pConfig.gates;
            config.switches = pConfig.switches;
        }

        public void RecordTimeStamp()
        {
            TimeStampData timeStampData = new TimeStampData();
            timeStampData.player = config.player.config.DeepCopy();
            timeStampData.boxes = config.boxes;
            timeStampData.walls = config.walls;
            timeStampData.gates = config.gates;
            timeStampData.switches = config.switches;

            config.timeStampData.Add(timeStampData);

            foreach(TimeStampData tsd in config.timeStampData)
            {
                Debug.Log(tsd.player.posX);
            }
            
            Debug.Log("\n");
        }
    }
}
