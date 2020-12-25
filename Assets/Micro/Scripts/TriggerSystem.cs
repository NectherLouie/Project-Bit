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
            public List<Player> players;
            public List<Box> boxes = new List<Box>();
            
            public List<Switch> switches = new List<Switch>();

            public void ResetGameObjects()
            {
                players.Clear();
                boxes.Clear();
                switches.Clear();
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();

            config.players = pConfig.players;
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
                foreach (Player p in config.players)
                {
                    if (sw.config.gridX == p.config.gridX && sw.config.gridY == p.config.gridY)
                    {
                        sw.ToggleSwitchOn();
                    }
                }

                foreach (Box b in config.boxes)
                {
                    if (sw.config.gridX == b.config.gridX && sw.config.gridY == b.config.gridY)
                    {
                        sw.ToggleSwitchOn();
                    }
                }
            }
        }
    }
}
