using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController main;

        public Camera camera;

        private void Awake()
        {
            main = this;
        }
    }
}
