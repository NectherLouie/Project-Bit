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
            public Player player;

            [HideInInspector]
            public List<Box> boxes = new List<Box>();

            [HideInInspector]
            public List<Wall> walls = new List<Wall>();

            [HideInInspector]
            public List<Switch> switches = new List<Switch>();
        }

        public Config config = new Config();

        public void Init()
        {
            foreach (Transform t in config.levelTransform)
            {
                if (t.GetComponent<Player>() != null)
                {
                    config.player = t.GetComponent<Player>();
                    config.player.gridX = (int)t.position.x;
                    config.player.gridY = (int)t.position.y;
                }

                if (t.GetComponent<Box>() != null)
                {
                    Box box = t.GetComponent<Box>();
                    box.gridX = (int)t.position.x;
                    box.gridY = (int)t.position.y;

                    config.boxes.Add(box);
                }

                if (t.GetComponent<Wall>() != null)
                {
                    Wall wall = t.GetComponent<Wall>();
                    wall.gridX = (int)t.position.x;
                    wall.gridY = (int)t.position.y;

                    config.walls.Add(wall);
                }

                if (t.GetComponent<Gate>() != null)
                {
                    Gate gate = t.GetComponent<Gate>();
                    gate.gridX = (int)t.position.x;
                    gate.gridY = (int)t.position.y;

                    config.walls.Add(gate);
                }

                if (t.GetComponent<Switch>() != null)
                {
                    Switch sw = t.GetComponent<Switch>();
                    sw.gridX = (int)t.position.x;
                    sw.gridY = (int)t.position.y;

                    config.switches.Add(sw);
                }
            }
        }
    }
}
