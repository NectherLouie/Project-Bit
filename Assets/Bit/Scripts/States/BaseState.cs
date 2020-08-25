using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class BaseState : MonoBehaviour
    {
        protected GameData gameData;
        protected GameObjectFactory gameObjectFactory;

        public void Inject(ref GameData pGameData, GameObjectFactory pGameObjectFactory)
        {
            gameData = pGameData;
            gameObjectFactory = pGameObjectFactory;
        }
    }
}
