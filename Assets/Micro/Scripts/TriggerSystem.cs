using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class TriggerSystem : MonoBehaviour
    {
        public Action<Exit> OnExitActivated;
        public Action<Switch> OnSwitchToggled;

        [Serializable]
        public class Config
        {
            public List<Player> players;
            public List<Box> boxes = new List<Box>();
            public List<Exit> exits = new List<Exit>();
            public List<Switch> switches = new List<Switch>();

            public void ResetGameObjects()
            {
                players.Clear();
                boxes.Clear();
                exits.Clear();
                switches.Clear();
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();

            config.players = pConfig.players;
            config.boxes = pConfig.boxes;
            config.exits = pConfig.exits;
            config.switches = pConfig.switches;
        }

        public void TriggerEvents()
        {
            // Exits
            foreach (Exit exit in config.exits)
            {
                exit.ToggleSwitchOff();
            }

            foreach (Exit exit in config.exits)
            {
                foreach(Player p in config.players)
                {
                    if (exit.config.gridX == p.config.gridX && exit.config.gridY == p.config.gridY)
                    {
                        OnExitActivated?.Invoke(exit);
                    }
                }

                foreach (Box b in config.boxes)
                {
                    if (exit.config.gridX == b.config.gridX && exit.config.gridY == b.config.gridY)
                    {
                        OnExitActivated?.Invoke(exit);
                    }
                }
            }

            // Switches and Gates
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
                        OnSwitchToggled?.Invoke(sw);
                    }
                }

                foreach (Box b in config.boxes)
                {
                    if (sw.config.gridX == b.config.gridX && sw.config.gridY == b.config.gridY)
                    {
                        OnSwitchToggled?.Invoke(sw);
                    }
                }
            }
        }
    }
}
