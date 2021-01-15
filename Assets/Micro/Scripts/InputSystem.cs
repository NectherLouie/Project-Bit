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
        public Action OnResetKeyDown;
        public Action OnRewindKeyDown;

        private bool inputEnabled = false;

        public void Init()
        {
            EnableInput(true);
        }

        public void EnableInput(bool pEnabled)
        {
            inputEnabled = pEnabled;
        }

        private void Update()
        {
            if (!inputEnabled)
            {
                return;
            }

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

            if (Input.GetKeyDown(KeyCode.R))
            {
                OnResetKeyDown?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                OnRewindKeyDown?.Invoke();
            }
        }
    }
}
