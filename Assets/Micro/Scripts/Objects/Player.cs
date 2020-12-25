using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class Player : Movable
    {
        [Serializable]
        public class Config
        {
            public int index = -1;

            public float posX = 0;
            public float posY = 0;

            public int gridX = 0;
            public int gridY = 0;

            public Config DeepCopy()
            {
                Config output = new Config();

                output.index = index;
                output.posX = posX;
                output.posY = posY;
                output.gridX = gridX;
                output.gridY = gridY;

                return output;
            }
        }

        public Config config = new Config();

        public Movable target;

        public Player DeepCopy()
        {
            Player output = new Player();
            output.config.index = config.index;
            output.config.posX = config.posX;
            output.config.posY = config.posY;
            output.config.gridX = config.gridX;
            output.config.gridY = config.gridY;

            output.target = target;

            return output;
        }

        public void Load(Vector2 pPos)
        {
            MovePosition(pPos.x, pPos.y);
            MoveGrid((int)pPos.x, (int)pPos.y);
        }

        public override void MovePosition(float pX, float pY)
        {
            config.posX += pX;
            config.posY += pY;
            transform.position = new Vector3(config.posX, config.posY, 0);
        }

        public override void MoveGrid(int pX, int pY)
        {
            config.gridX += pX;
            config.gridY += pY;
        }
    }
}
