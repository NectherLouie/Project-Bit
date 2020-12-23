using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class Movable : MonoBehaviour
    {
        public bool hasTriggeredSwitch = false;

        public virtual void MovePosition(float pX, float pY)
        {

        }

        public virtual void MoveGrid(int pX, int pY)
        {

        }

        public virtual void SwitchOn(Switch pSwitch)
        {

        }

        public virtual void SwitchOff(Switch pSwitch)
        {

        }
    }
}
