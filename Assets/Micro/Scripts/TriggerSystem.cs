using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class TriggerSystem : MonoBehaviour
    {
        [Serializable]
        public class Config
        {
            public Player player;
            public List<Box> boxes = new List<Box>();
            
            public List<Switch> switches = new List<Switch>();

            public void ResetGameObjects()
            {
                player = null;
                boxes.Clear();
                switches.Clear();
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();

            config.player = pConfig.player;
            config.boxes = pConfig.boxes;
            config.switches = pConfig.switches;
        }

        public void TriggerEvents()
        {
            foreach (Switch sw in config.switches)
            {
                sw.ToggleSwitchOff();
            }

            foreach (Switch sw in config.switches)
            {
                foreach (Box b in config.boxes)
                {
                    if (sw.gridX == b.gridX && sw.gridY == b.gridY)
                    {
                        sw.ToggleSwitchOn();
                    }
                }

                if(sw.gridX == config.player.gridX && sw.gridY == config.player.gridY)
                {
                    sw.ToggleSwitchOn();
                }
            }
        }
    }
}
