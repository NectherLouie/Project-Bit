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
                    if (sw.config.gridX == b.config.gridX && sw.config.gridY == b.config.gridY)
                    {
                        sw.ToggleSwitchOn();
                    }
                }

                if(sw.config.gridX == config.player.config.gridX && sw.config.gridY == config.player.config.gridY)
                {
                    sw.ToggleSwitchOn();
                }
            }
        }
    }
}
