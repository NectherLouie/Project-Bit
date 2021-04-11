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
        public Action<CoinMultiplier> OnCoinMultiplierToggled;
        public Action<Entrance> OnEntranceActivated;

        [Serializable]
        public class Config
        {
            public List<Player> players;
            public List<Box> boxes = new List<Box>();
            public List<Exit> exits = new List<Exit>();
            public List<Switch> switches = new List<Switch>();
            public List<CoinMultiplier> coinMultipliers = new List<CoinMultiplier>();
            public List<Entrance> entrances = new List<Entrance>();

            public void ResetGameObjects()
            {
                players.Clear();
                boxes.Clear();
                exits.Clear();
                switches.Clear();
                coinMultipliers.Clear();
                entrances.Clear();
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.players = pConfig.players;
            config.boxes = pConfig.boxes;
            config.exits = pConfig.exits;
            config.switches = pConfig.switches;
            config.coinMultipliers = pConfig.coinMultipliers;
            config.entrances = pConfig.entrances;
        }

        public void Unload()
        {
            config.ResetGameObjects();
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

            // Coin Multiplier
            foreach (CoinMultiplier cm in config.coinMultipliers)
            {
                foreach (Player p in config.players)
                {
                    if (cm.config.gridX == p.config.gridX && cm.config.gridY == p.config.gridY)
                    {
                        OnCoinMultiplierToggled?.Invoke(cm);
                    }
                }
            }

            // Entrances
            foreach (Entrance entrance in config.entrances)
            {
                foreach (Player p in config.players)
                {
                    if (entrance.config.gridX == p.config.gridX && entrance.config.gridY == p.config.gridY)
                    {
                        OnEntranceActivated?.Invoke(entrance);
                    }
                }
            }
        }
    }
}
