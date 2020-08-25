﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class Wall : MonoBehaviour
    {
        public int gridX = 0;
        public int gridY = 0;

        public GridData gridData;

        public void Load(int pGridX, int pGridY, Vector2 pPos)
        {
            gridX = pGridX;
            gridY = pGridY;

            Vector3 pos = transform.position;
            pos.x = pPos.x;
            pos.y = pPos.y;
            transform.position = pos;
        }

        public void Move(float pX, float pY)
        {
            Vector3 position = transform.position;
            position.x += pX;
            position.y += pY;
            transform.position = position;
        }

        public void MoveGrid(int pX, int pY)
        {
            gridX += pX;
            gridY += pY;
        }
    }
}