using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bit
{
    public class Lock : MonoBehaviour
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

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.25f)
                .SetDelay(Random.Range(0, 0.15f));
        }
    }
}
