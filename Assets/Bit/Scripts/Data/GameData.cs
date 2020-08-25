using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
    public class GameData : ScriptableObject
    {
        public GameObject levelObject;
        public int levelIndex = 0;
        public int lastLevelIndex = 0;

        public Vector2 cellSize = Vector2.one;

        public List<GridData> gridData = new List<GridData>();

        public void Reset()
        {
            levelObject = null;
            levelIndex = 0;
            lastLevelIndex = 0;

            cellSize = new Vector2(1f, 1f);

            gridData = new List<GridData>();
        }
    }
}
