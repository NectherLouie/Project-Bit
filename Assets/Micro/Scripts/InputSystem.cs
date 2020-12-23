using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class InputSystem : MonoBehaviour
    {
        public Action<int, int> OnMoveUp;
        public Action<int, int> OnMoveDown;
        public Action<int, int> OnMoveRight;
        public Action<int, int> OnMoveLeft;

        public void Init()
        {
            //
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnMoveUp.Invoke(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnMoveDown.Invoke(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnMoveRight.Invoke(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnMoveLeft?.Invoke(0, -1);
            }
        }
    }
}
