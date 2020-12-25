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
            public List<Switch> switches = new List<Switch>();

            //[HideInInspector]
            public List<Gate> gates = new List<Gate>();
        }

        public Config config = new Config();

        public void Init()
        {
            int playerCount = 0;
            int boxCount = 0;
            int wallCount = 0;
            int gateCount = 0;
            int switchCount = 0;

            foreach (Transform t in config.levelTransform)
            {
                if (t.GetComponent<Player>() != null)
                {
                    Player player = t.GetComponent<Player>();
                    player.Load(playerCount, t.position);

                    config.players.Add(player);

                    playerCount++;
                }

                if (t.GetComponent<Box>() != null)
                {
                    Box box = t.GetComponent<Box>();
                    box.Load(boxCount, t.position);

                    config.boxes.Add(box);

                    boxCount++;
                }

                if (t.GetComponent<Wall>() != null)
                {
                    Wall wall = t.GetComponent<Wall>();
                    wall.Load(wallCount, t.position);

                    config.walls.Add(wall);

                    wallCount++;
                }

                if (t.GetComponent<Gate>() != null)
                {
                    Gate gate = t.GetComponent<Gate>();
                    gate.Load(gateCount, t.position);

                    config.gates.Add(gate);

                    gateCount++;
                }

                if (t.GetComponent<Switch>() != null)
                {
                    Switch sw = t.GetComponent<Switch>();
                    sw.Load(switchCount, t.position);

                    config.switches.Add(sw);

                    switchCount++;
                }
            }
        }
    }
}
