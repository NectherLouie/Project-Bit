using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class LoadState : BaseState
    {
        public event Action OnComplete;

        public void Enter()
        {
            gameObjectFactory.OnLoadComplete += LoadComplete;
            gameObjectFactory.Load();
        }

        private void LoadComplete()
        {
            gameObjectFactory.OnLoadComplete -= LoadComplete;

            gameData.lastLevelIndex = gameObjectFactory.GetLastLevelIndex();

            OnComplete?.Invoke();
        }
    }
}
