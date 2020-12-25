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

            //[HideInInspector]
            public List<Gate> gates = new List<Gate>();
        }

        public Config config = new Config();

        public void Init()
        {
            foreach (Transform t in config.levelTransform)
            {
                if (t.GetComponent<Player>() != null)
                {
                    config.player = t.GetComponent<Player>();
                    config.player.Load(t.position);
                }

                if (t.GetComponent<Box>() != null)
                {
                    Box box = t.GetComponent<Box>();
                    box.Load(t.position);

                    config.boxes.Add(box);
                }

                if (t.GetComponent<Wall>() != null)
                {
                    Wall wall = t.GetComponent<Wall>();
                    wall.Load(t.position);

                    config.walls.Add(wall);
                }

                if (t.GetComponent<Gate>() != null)
                {
                    Gate gate = t.GetComponent<Gate>();
                    gate.Load(t.position);

                    config.gates.Add(gate);
                }

                if (t.GetComponent<Switch>() != null)
                {
                    Switch sw = t.GetComponent<Switch>();
                    sw.Load(t.position);

                    config.switches.Add(sw);
                }
            }
        }
    }
}
