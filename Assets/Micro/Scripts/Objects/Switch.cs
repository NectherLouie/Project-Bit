using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class Switch : Movable
    {
        public List<Movable> pairedGates = new List<Movable>();

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

        public void ToggleSwitchOn()
        {
            foreach(Gate gate in pairedGates)
            {
                gate.SwitchOn(this);
            }
        }

        public void ToggleSwitchOff()
        {
            foreach (Gate gate in pairedGates)
            {
                gate.SwitchOff(this);
            }
        }
    }
}
