using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class MovementSystem : MonoBehaviour
    {
        public Action OnMoveSucceeded;
        public Action OnMoveComplete;

        [Serializable]
        public class Config
        {
            public Vector2 cellSize = Vector2.one;
            public int horizontal = 0;
            public int vertical = 0;

            public List<Player> players = new List<Player>();
            public List<Box> boxes = new List<Box>();
            public List<Wall> walls = new List<Wall>();
            public List<Gate> gates = new List<Gate>();

            public void ResetGameObjects()
            {
                players.Clear();
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

        public Config config = new Config();

        public void Init(LevelSystem.Config pConfig)
        {
            config.players = pConfig.players;
            config.boxes = pConfig.boxes;
            config.walls = pConfig.walls;
            config.gates = pConfig.gates;
        }

        public void Unload()
        {
            config.ResetGameObjects();
            config.ResetPlayData();
        }

        public void Move(int pVertical, int pHorizontal)
        {
            config.vertical = pVertical;
            config.horizontal = pHorizontal;

            List<bool> playerCanMove = ResetPlayerCanMoveList();

            if (config.vertical != 0)
            {
                TryMoveVertical(ref playerCanMove);
            }

            if (config.horizontal != 0)
            {
                TryMoveHorizontal(ref playerCanMove);
            }

            if (playerCanMove.Contains(true))
            {
                OnMoveSucceeded?.Invoke();
                UpdateMovements(ref playerCanMove);
            }
        }

        private List<bool> ResetPlayerCanMoveList()
        {
            List<bool> output = new List<bool>();

            for (int i = 0; i < config.players.Count; ++i)
            {
                output.Add(true);
            }

            return output;
        }

        private void TryMoveVertical(ref List<bool> pPlayerCanMoveList)
        {
            // Check for each players
            for (int playerIndex = 0; playerIndex  < config.players.Count; ++playerIndex)
            {
                Player player = config.players[playerIndex];

                int playerMoveX = player.config.gridX;
                int playerMoveY = player.config.gridY + config.vertical;
                
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
                            player.target = box;
                        }
                    }
                }

                config.players[playerIndex] = player;

                pPlayerCanMoveList[playerIndex] = canMove;
            }
        }

        private void TryMoveHorizontal(ref List<bool> pPlayerCanMoveList)
        {
            for (int playerIndex = 0; playerIndex < config.players.Count; ++playerIndex)
            {
                Player player = config.players[playerIndex];

                int playerMoveX = player.config.gridX + config.horizontal;
                int playerMoveY = player.config.gridY;

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
                            player.target = box;
                        }
                    }
                }

                config.players[playerIndex] = player;

                pPlayerCanMoveList[playerIndex] = canMove;
            }
        }

        public void UpdateMovements(ref List<bool> pPlayerCanMoveList)
        {
            for (int playerIndex = 0; playerIndex < config.players.Count; ++playerIndex)
            {
                Player player = config.players[playerIndex];
                bool canMove = pPlayerCanMoveList[playerIndex];

                if (canMove)
                {
                    float playerMoveX = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
                    float playerMoveY = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;
            
                    player.MovePosition(playerMoveX, playerMoveY);

                    int playerGridX = config.horizontal != 0 ? config.horizontal : 0;
                    int playerGridY = config.vertical != 0 ? config.vertical : 0;

                    player.MoveGrid(playerGridX, playerGridY);

                    if (player.target != null)
                    {
                        Movable playerTarget = player.target;

                        float targetMoveX = config.horizontal != 0 ? config.horizontal * config.cellSize.x : 0;
                        float targetMoveY = config.vertical != 0 ? config.vertical * config.cellSize.y : 0;

                        playerTarget.MovePosition(targetMoveX, targetMoveY);

                        int targetGridX = config.horizontal != 0 ? config.horizontal : 0;
                        int targetGridY = config.vertical != 0 ? config.vertical : 0;

                        playerTarget.MoveGrid(targetGridX, targetGridY);

                        player.target = null;
                    }
                }
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
