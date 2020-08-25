using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit
{
    public class LevelMap : MonoBehaviour
    {
        public List<GameObject> levelObjects = new List<GameObject>();

        private Player player;
        private List<Box> boxes = new List<Box>();
        private List<Wall> walls = new List<Wall>();
        private List<Goal> goals = new List<Goal>();
        private List<Key> keys = new List<Key>();
        private List<Lock> locks = new List<Lock>();

        public void Load(Vector2 pCellSize)
        {
            for (int childIndex = 0; childIndex < transform.childCount; ++childIndex)
            {
                GameObject child = transform.GetChild(childIndex).gameObject;

                if (child.GetComponent<Player>() != null)
                {
                    Player player = child.GetComponent<Player>();

                    Vector3 playerPosition = player.transform.position;

                    int playerGridX = (int)(playerPosition.x / pCellSize.x);
                    int playerGridY = (int)(playerPosition.y / pCellSize.y);

                    player.gridData.position = new Vector3(playerGridX, playerGridY, 0f);
                }

                if (child.GetComponent<Box>() != null)
                {
                    Box box = child.GetComponent<Box>();

                    Vector3 boxPosition = box.transform.position;
                    int boxGridX = (int)(boxPosition.x / pCellSize.x);
                    int boxGridY = (int)(boxPosition.y / pCellSize.y);

                    box.gridData.position = new Vector3(boxGridX, boxGridY, 0f);
                }

                if (child.GetComponent<Wall>() != null)
                {
                    Wall wall = child.GetComponent<Wall>();

                    Vector3 wallPosition = wall.transform.position;
                    int wallGridX = (int)(wallPosition.x / pCellSize.x);
                    int wallGridY = (int)(wallPosition.y / pCellSize.y);

                    wall.gridData.position = new Vector3(wallGridX, wallGridY, 0f);
                }

                if (child.GetComponent<Goal>() != null)
                {
                    Goal goal = child.GetComponent<Goal>();

                    Vector3 goalPosition = goal.transform.position;
                    int goalGridX = (int)(goalPosition.x / pCellSize.x);
                    int goalGridY = (int)(goalPosition.y / pCellSize.y);

                    goal.gridData.position = new Vector3(goalGridX, goalGridY, 0f);
                }

                if (child.GetComponent<Key>() != null)
                {
                    Key key = child.GetComponent<Key>();

                    Vector3 keyPosition = key.transform.position;
                    int keyGridX = (int)(keyPosition.x / pCellSize.x);
                    int keyGridY = (int)(keyPosition.y / pCellSize.y);

                    key.gridData.position = new Vector3(keyGridX, keyGridY, 0f);
                }

                if (child.GetComponent<Lock>() != null)
                {
                    Lock _lock = child.GetComponent<Lock>();

                    Vector3 lockPosition = _lock.transform.position;
                    int lockGridX = (int)(lockPosition.x / pCellSize.x);
                    int lockGridY = (int)(lockPosition.y / pCellSize.y);

                    _lock.gridData.position = new Vector3(lockGridX, lockGridY, 0f);
                }

                levelObjects.Add(child);
            }
        }

        public void LoadLevel(Vector2 pCellSize)
        {
            // TODO: Reset Grid
            player = null;
            boxes.Clear();
            walls.Clear();
            goals.Clear();
            keys.Clear();
            locks.Clear();

            foreach (GameObject g in levelObjects)
            {
                if (g.GetComponent<Player>() != null)
                {
                    player = g.GetComponent<Player>();

                    Vector3 playerPosition = player.gridData.position;
                    
                    int playerGridX = (int)(playerPosition.x / pCellSize.x);
                    int playerGridY = (int)(playerPosition.y / pCellSize.y);

                    player.Load(playerGridX, playerGridY, new Vector2(playerPosition.x, playerPosition.y));
                }

                if (g.GetComponent<Box>() != null)
                {
                    Box box = g.GetComponent<Box>();

                    Vector3 boxPosition = box.gridData.position;

                    int boxGridX = (int)(boxPosition.x / pCellSize.x);
                    int boxGridY = (int)(boxPosition.y / pCellSize.y);

                    box.Load(boxGridX, boxGridY, new Vector2(boxPosition.x, boxPosition.y));

                    boxes.Add(box);
                }

                if (g.GetComponent<Wall>() != null)
                {
                    Wall wall = g.GetComponent<Wall>();

                    Vector3 wallPosition = wall.gridData.position;

                    int wallGridX = (int)(wallPosition.x / pCellSize.x);
                    int wallGridY = (int)(wallPosition.y / pCellSize.y);

                    wall.Load(wallGridX, wallGridY, new Vector2(wallPosition.x, wallPosition.y));

                    walls.Add(wall);
                }

                if (g.GetComponent<Goal>() != null)
                {
                    Goal goal = g.GetComponent<Goal>();

                    Vector3 goalPosition = goal.gridData.position;

                    int goalGridX = (int)(goalPosition.x / pCellSize.x);
                    int goalGridY = (int)(goalPosition.y / pCellSize.y);

                    goal.Load(goalGridX, goalGridY, new Vector2(goalPosition.x, goalPosition.y));

                    goals.Add(goal);
                }

                if (g.GetComponent<Key>() != null)
                {
                    Key key = g.GetComponent<Key>();

                    Vector3 keyPosition = key.gridData.position;

                    int keyGridX = (int)(keyPosition.x / pCellSize.x);
                    int keyGridY = (int)(keyPosition.y / pCellSize.y);

                    key.Load(keyGridX, keyGridY, new Vector2(keyPosition.x, keyPosition.y));

                    keys.Add(key);
                }

                if (g.GetComponent<Lock>() != null)
                {
                    Lock _lock = g.GetComponent<Lock>();

                    Vector3 lockPosition = _lock.gridData.position;

                    int lockGridX = (int)(lockPosition.x / pCellSize.x);
                    int lockGridY = (int)(lockPosition.y / pCellSize.y);

                    _lock.Load(lockGridX, lockGridY, new Vector2(lockPosition.x, lockPosition.y));

                    locks.Add(_lock);
                }
            }
        }

        public Player GetPlayer()
        {
            return player;
        }

        public List<Box> GetBoxes()
        {
            return boxes;
        }

        public List<Wall> GetWalls()
        {
            return walls;
        }

        public List<Goal> GetGoals()
        {
            return goals;
        }

        public List<Key> GetKeys()
        {
            foreach(Key k in keys)
            {
                k.gameObject.SetActive(true);
            }

            return keys;
        }

        public List<Lock> GetLocks()
        {
            foreach(Lock l in locks)
            {
                l.gameObject.SetActive(true);
            }

            return locks;
        }
    }
}
