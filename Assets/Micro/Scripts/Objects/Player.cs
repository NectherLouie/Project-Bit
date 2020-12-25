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

            public Config Clone()
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

        public void Load(int pIndex, Vector2 pPos)
        {
            config.index = pIndex;

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
