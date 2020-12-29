using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class Switch : Movable
    {
        public List<Movable> pairedGates = new List<Movable>();

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
