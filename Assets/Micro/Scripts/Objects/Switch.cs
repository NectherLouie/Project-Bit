using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class Switch : Movable
    {
        public int gridX = 0;
        public int gridY = 0;

        public List<Movable> pairedGates = new List<Movable>();

        public void Load(int pGridX, int pGridY, Vector2 pPos)
        {
            gridX = pGridX;
            gridY = pGridY;

            Vector3 pos = transform.position;
            pos.x = pPos.x;
            pos.y = pPos.y;
            transform.position = pos;
        }

        public override void MovePosition(float pX, float pY)
        {
            Vector3 position = transform.position;
            position.x += pX;
            position.y += pY;
            transform.position = position;
        }

        public override void MoveGrid(int pX, int pY)
        {
            gridX += pX;
            gridY += pY;
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
