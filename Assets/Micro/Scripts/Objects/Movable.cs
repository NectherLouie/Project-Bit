using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class Movable : MonoBehaviour
    {
        [Serializable]
        public class Config
        {
            public int index = -1;
            public float posX = 0;
            public float posY = 0;
            public int gridX = 0;
            public int gridY = 0;

            public Config Clone()
            {
                Config output = new Config();

                output.index = index;
                output.posX = posX;
                output.posY = posY;
                output.gridX = gridX;
                output.gridY = gridY;

                return output;
            }
        }

        public Config config = new Config();

        public bool hasTriggeredSwitch = false;

        public virtual void Load(int pIndex, Vector2 pPos)
        {
            config.index = pIndex;

            config.posX = pPos.x;
            config.posY = pPos.y;
            transform.localPosition = new Vector3(config.posX, config.posY, 0);

            config.gridX = (int)pPos.x;
            config.gridY = (int)pPos.y;
        }

        public virtual void LoadTimeStamp(Vector2 pPos)
        {
            config.posX = pPos.x;
            config.posY = pPos.y;
            transform.localPosition = new Vector3(config.posX, config.posY, 0);

            config.gridX = (int)pPos.x;
            config.gridY = (int)pPos.y;
        }

        public virtual void MovePosition(float pX, float pY)
        {
            config.posX += pX;
            config.posY += pY;
            //transform.localPosition = new Vector3(config.posX, config.posY, 0);

            transform.DOLocalMove(new Vector3(config.posX, config.posY, 0), 0.15f)
                .OnComplete(OnMoveComplete);
        }

        private void OnMoveComplete()
        {
            transform.localPosition = new Vector3(config.posX, config.posY, 0);
        }

        public virtual void MoveGrid(int pX, int pY)
        {
            config.gridX += pX;
            config.gridY += pY;
        }

        public virtual void SwitchOn(Switch pSwitch)
        {

        }

        public virtual void SwitchOff(Switch pSwitch)
        {

        }
    }
}
