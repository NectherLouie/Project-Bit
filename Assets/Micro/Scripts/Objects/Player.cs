using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class Player : MonoBehaviour
    {
        public int gridX = 0;
        public int gridY = 0;
        public Movable target;

        public void Load(int pGridX, int pGridY, Vector2 pPos)
        {
            gridX = pGridX;
            gridY = pGridY;

            Vector3 pos = transform.position;
            pos.x = pPos.x;
            pos.y = pPos.y;
            transform.position = pos;

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.25f)
                .SetDelay(Random.Range(0, 0.15f));
        }

        public void MovePosition(float pX, float pY)
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
