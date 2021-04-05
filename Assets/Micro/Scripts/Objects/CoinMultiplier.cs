using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class CoinMultiplier : Movable
    {
        public int multiplier = 1;
        public TextMesh textMesh;

        public override void Load(int pIndex, Vector2 pPos)
        {
            base.Load(pIndex, pPos);

            textMesh.text = "X" + multiplier.ToString();
        }
    }
}
