using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class MovementSystem : MonoBehaviour
    {
        public Action OnMoveComplete;

        [Serializable]
        public class Config
        {
            public Vector2 cellSize = Vector2.one;
            public int horizontal = 0;
            public int vertical = 0;

            public Player player;
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();

            public void ResetGameObjects()
            {
                player = null;
                boxes.Clear();
                walls.Clear();
            }

            public void ResetPlayData()
            {
                cellSize = Vector2.one;
                vertical = 0;
                horizontal = 0;
            }
        }

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();
            config.ResetPlayData();

            config.player = pConfig.player;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
        }

        public void Move(int pVertical, int pHorizontal)
        {
            config.vertical = pVertical;
            config.horizontal = pHorizontal;

            bool playerCanMove = true;

            if (config.vertical != 0)
            {
                playerCanMove = TryMoveVertical();
            }

            if (config.horizontal != 0)
            {
                playerCanMove = TryMoveHorizontal();
            }

            if (playerCanMove)
            {
                UpdateMovements();
            }
        }

        private bool TryMoveVertical()
        {
            int playerMoveX = config.player.gridX;
            int playerMoveY = config.player.gridY + config.vertical;

            // Check impassables
            bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
            bool canMove = !isPlayerCollidingWalls;

            // Check movables
            for (int boxIndex = 0; boxIndex < config.boxes.Count; ++boxIndex)
            {
                Box box = config.boxes[boxIndex];

                if (box.gridX == playerMoveX && box.gridY == playerMoveY)
                {
                    int boxMoveX = box.gridX;
                    int boxMoveY = box.gridY + config.vertical;

                    bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                    bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);

                    canMove = !isBoxCollidingWall && !isBoxCollidingBox;

                    if (canMove)
                    {
                        config.player.target = box;
                    }
                }
            }

            return canMove;
        }

        private bool TryMoveHorizontal()
        {
            int playerMoveX = config.player.gridX + config.horizontal;
            int playerMoveY = config.player.gridY;

            // check impassables
            bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
            bool canMove = !isPlayerCollidingWalls;

            // Check movables
            for (int boxIndex = 0; boxIndex < config.boxes.Count; ++boxIndex)
            {
                Box box = config.boxes[boxIndex];

                if (box.gridX == playerMoveX && box.gridY == playerMoveY)
                {
                    int boxMoveX = box.gridX + config.horizontal;
                    int boxMoveY = box.gridY;

                    bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                    bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);

                    canMove = !isBoxCollidingWall && !isBoxCollidingBox;

                    if (canMove)
                    {
                        config.player.target = box;
                    }
                }
            }

            return canMove;
        }

        public void UpdateMovements()
        {
            float movePlayerX = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
            float movePlayerY = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;

            config.player.MovePosition(movePlayerX, movePlayerY);

            int moveGridX = config.horizontal != 0 ? config.horizontal : 0;
            int moveGridY = config.vertical != 0 ? config.vertical : 0;

            config.player.MoveGrid(moveGridX, moveGridY);

            if (config.player.target != null)
            {
                Movable playerTarget = config.player.target;

                float moveTargetX = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
                float moveTargetY = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;

                playerTarget.MovePosition(moveTargetX, moveTargetY);

                int gridTargetX = config.horizontal != 0 ? config.horizontal : 0;
                int gridTargetY = config.vertical != 0 ? config.vertical : 0;

                playerTarget.MoveGrid(gridTargetX, gridTargetY);

                config.player.target = null;
            }

            config.vertical = 0;
            config.horizontal = 0;

            OnMoveComplete?.Invoke();
        }

        private bool IsCollidingWalls(int gridX, int gridY)
        {
            bool output = false;

            for (int wallIndex = 0; wallIndex < config.walls.Count; ++wallIndex)
            {
                var wall = config.walls[wallIndex];

                if (wall.gridX == gridX && wall.gridY == gridY)
                {
                    output = true;

                    if (wall.hasTriggeredSwitch)
                    {
                        output = false;
                    }
                }
            }

            return output;
        }

        private bool IsCollidingBoxes(int gridX, int gridY)
        {
            bool output = false;

            for (int boxIndex = 0; boxIndex < config.boxes.Count; ++boxIndex)
            {
                Box box = config.boxes[boxIndex];

                if (box.gridX == gridX && box.gridY == gridY)
                {
                    output = true;

                    if (box.hasTriggeredSwitch)
                    {
                        output = false;
                    }
                }
            }

            return output;
        }
    }
}
