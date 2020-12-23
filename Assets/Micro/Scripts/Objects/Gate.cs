using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class Gate : Wall
    {
        public enum Condition { AND, OR }
        public Condition condition;

        public List<Switch> pairedSwitches = new List<Switch>();

        private int switchCount = 0;

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

        public override void SwitchOn(Switch pSwitch)
        {
            // Check if switch and gate is paired
            bool incrementCount = false;
            foreach (Switch pairedSwitch in pairedSwitches)
            {
                if (pairedSwitch.gameObject.name == pSwitch.gameObject.name)
                {
                    incrementCount = true;
                    break;
                }
            }

            // Do switch
            if (incrementCount)
            {
                switchCount++;
                if (condition == Condition.AND)
                {
                    if (switchCount >= pairedSwitches.Count)
                    {
                        switchCount = pairedSwitches.Count;

                        hasTriggeredSwitch = true;

                        gameObject.SetActive(false);
                    }
                }

                if (condition == Condition.OR)
                {
                    hasTriggeredSwitch = true;

                    gameObject.SetActive(false);
                }
            }
        }

        public override void SwitchOff(Switch pSwitch)
        {
            // Check if switch and gate is paired
            bool decrementCount = false;
            foreach (Switch pairedSwitch in pairedSwitches)
            {
                if (pairedSwitch.gameObject.name == pSwitch.gameObject.name)
                {
                    decrementCount = true;
                    break;
                }
            }

            // Do switch
            if (decrementCount)
            {
                if (condition == Condition.AND)
                {

                    switchCount--;
                    if (switchCount <= 0)
                    {
                        switchCount = 0;

                        hasTriggeredSwitch = false;

                        gameObject.SetActive(true);
                    }
                }

                if (condition == Condition.OR)
                {
                    hasTriggeredSwitch = false;

                    gameObject.SetActive(true);
                }
            }
        }
    }
}
