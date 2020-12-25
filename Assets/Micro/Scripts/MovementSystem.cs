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
            public List<Gate> gates = new List<Gate>();

            public void ResetGameObjects()
            {
                player = null;
                boxes.Clear();
                walls.Clear();
                gates.Clear();
            }

            public void ResetPlayData()
            {
                cellSize = Vector2.one;
                vertical = 0;
                horizontal = 0;
            }
        }

        public class MoveConfig
        {
            public Vector2 playerMove;
            public Vector2 playerGrid;

            public Vector2 playerTargetMove;
            public Vector2 playerTargetGrid;
        }

        public Config config = new Config();

        private MoveConfig moveConfig = new MoveConfig();

        public void Init(LevelSystem.Config pConfig)
        {
            config.ResetGameObjects();
            config.ResetPlayData();

            config.player = pConfig.player;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
            config.gates = pConfig.gates;
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
            int playerMoveX = config.player.config.gridX;
            int playerMoveY = config.player.config.gridY + config.vertical;

            // Check impassables
            bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
            bool isPlayerCollidingGates = IsCollidingGates(playerMoveX, playerMoveY);
            bool canMove = !isPlayerCollidingWalls && !isPlayerCollidingGates;

            // Check movables
            for (int boxIndex = 0; boxIndex < config.boxes.Count; ++boxIndex)
            {
                Box box = config.boxes[boxIndex];

                if (box.config.gridX == playerMoveX && box.config.gridY == playerMoveY)
                {
                    int boxMoveX = box.config.gridX;
                    int boxMoveY = box.config.gridY + config.vertical;

                    bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                    bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);
                    bool isBoxCollidingGates = IsCollidingGates(boxMoveX, boxMoveY);

                    canMove = !isBoxCollidingWall && !isBoxCollidingBox && !isBoxCollidingGates;

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
            int playerMoveX = config.player.config.gridX + config.horizontal;
            int playerMoveY = config.player.config.gridY;

            // check impassables
            bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
            bool isPlayerCollidingGates = IsCollidingGates(playerMoveX, playerMoveY);
            bool canMove = !isPlayerCollidingWalls && !isPlayerCollidingGates;

            // Check movables
            for (int boxIndex = 0; boxIndex < config.boxes.Count; ++boxIndex)
            {
                Box box = config.boxes[boxIndex];

                if (box.config.gridX == playerMoveX && box.config.gridY == playerMoveY)
                {
                    int boxMoveX = box.config.gridX + config.horizontal;
                    int boxMoveY = box.config.gridY;

                    bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                    bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);
                    bool isBoxCollidingGates = IsCollidingGates(boxMoveX, boxMoveY);

                    canMove = !isBoxCollidingWall && !isBoxCollidingBox && !isBoxCollidingGates;

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
            moveConfig.playerMove.x = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
            moveConfig.playerMove.y = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;
            
            config.player.MovePosition(moveConfig.playerMove.x, moveConfig.playerMove.y);

            moveConfig.playerGrid.x = config.horizontal != 0 ? config.horizontal : 0;
            moveConfig.playerGrid.y = config.vertical != 0 ? config.vertical : 0;

            config.player.MoveGrid((int)moveConfig.playerGrid.x, (int)moveConfig.playerGrid.y);

            if (config.player.target != null)
            {
                Movable playerTarget = config.player.target;

                moveConfig.playerTargetMove.x = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
                moveConfig.playerTargetMove.y = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;

                playerTarget.MovePosition(moveConfig.playerTargetMove.x, moveConfig.playerTargetMove.y);

                moveConfig.playerTargetGrid.x = config.horizontal != 0 ? config.horizontal : 0;
                moveConfig.playerTargetGrid.y = config.vertical != 0 ? config.vertical : 0;

                playerTarget.MoveGrid((int)moveConfig.playerTargetGrid.x, (int)moveConfig.playerTargetGrid.y);

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

                if (wall.config.gridX == gridX && wall.config.gridY == gridY)
                {
                    output = true;
                }
            }

            return output;
        }

        private bool IsCollidingGates(int gridX, int gridY)
        {
            bool output = false;

            for (int i = 0; i < config.gates.Count; ++i)
            {
                var gate = config.gates[i];

                if (gate.config.gridX == gridX && gate.config.gridY == gridY)
                {
                    output = true;

                    if (gate.hasTriggeredSwitch)
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

                if (box.config.gridX == gridX && box.config.gridY == gridY)
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
