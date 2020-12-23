using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Bit
{
    public class LevelPlayState : BaseState
    {
        public event Action OnComplete;
        public event Action OnRestartEvent;

        private Vector2 cellSize;

        private Player _activePlayer;
        private List<Box> _activeBoxes = new List<Box>();
        private List<Wall> _activeWalls = new List<Wall>();
        private List<Goal> _activeGoals = new List<Goal>();
        private List<Key> _activeKeys = new List<Key>();
        private List<Lock> _activeLocks = new List<Lock>();

        private bool levelComplete = false;
        private bool enableUpdate = false;

        private LevelPlayPanel levelPlayPanel;
        private SFXPlayer sfxPlayer;

        public void Enter()
        {
            if (gameData.levelObject != null)
            {
                gameData.levelObject.SetActive(false);
                gameData.levelObject = null;
            }

            gameData.levelObject = gameObjectFactory.FetchLevel();
            gameData.levelObject.SetActive(true);

            LevelMap levelMap = gameData.levelObject.GetComponent<LevelMap>();
            _activePlayer = levelMap.GetPlayer();
            _activeBoxes = levelMap.GetBoxes();
            _activeWalls = levelMap.GetWalls();
            _activeGoals = levelMap.GetGoals();
            _activeKeys = levelMap.GetKeys();
            _activeLocks = levelMap.GetLocks();

            cellSize = gameData.cellSize;

            levelComplete = false;
            enableUpdate = true;

            levelPlayPanel = gameObjectFactory.levelPlayPanelObject.GetComponent<LevelPlayPanel>();
            levelPlayPanel.Show();

            sfxPlayer = gameObjectFactory.sfxPlayerObject.GetComponent<SFXPlayer>();
        }

        private void Update()
        {
            if (enableUpdate)
            {
                StateUpdate();
            }
        }

        private void StateUpdate()
        {
            if (!levelComplete)
            {
                InputEvents();
                LockKeyEvents();
                WinEvents();
            }
            else
            {
                PlayComplete();
            }
        }

        private void InputEvents()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                sfxPlayer.PlayResetSound();
                RestartLevel();
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MovePlayer(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MovePlayer(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePlayer(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePlayer(0, -1);
            }
        }

        private void LockKeyEvents()
        {
            List<Key> activeKeys = _activeKeys;
            List<Lock> activeLocks = _activeLocks;

            int keyRemoveIndex = -1;
            int lockRemoveIndex = -1;

            for (int keyIndex = 0; keyIndex < activeKeys.Count; ++keyIndex)
            {
                for (int lockIndex = 0; lockIndex < activeLocks.Count; ++lockIndex)
                {
                    Key _key = activeKeys[keyIndex];
                    Lock _lock = activeLocks[lockIndex];

                    if (_key.gridX == _lock.gridX && _key.gridY == _lock.gridY)
                    {
                        _key.gameObject.SetActive(false);
                        _lock.gameObject.SetActive(false);

                        keyRemoveIndex = keyIndex;
                        lockRemoveIndex = lockIndex;

                        sfxPlayer.PlayKeySound();
                    }
                }
            }

            if (keyRemoveIndex != -1)
            {
                activeKeys.RemoveAt(keyRemoveIndex);
            }

            if (lockRemoveIndex != -1)
            {
                activeLocks.RemoveAt(lockRemoveIndex);
            }
        }

        private void WinEvents()
        {
            int winTarget = _activeGoals.Count;
            int winCount = 0;

            for (int goalIndex = 0; goalIndex < _activeGoals.Count; ++goalIndex)
            {
                Goal goal = _activeGoals[goalIndex];

                for (int boxIndex = 0; boxIndex < _activeBoxes.Count; ++boxIndex)
                {
                    Box box = _activeBoxes[boxIndex];

                    if (goal.gridX == box.gridX && goal.gridY == box.gridY)
                    {
                        winCount++;
                    }
                }
            }

            if (winCount >= winTarget)
            {
                sfxPlayer.PlayBoxSound();
                levelComplete = true;
            }
        }

        private void PlayComplete()
        {
            enableUpdate = false;
            
            levelPlayPanel.Hide();

            OnComplete?.Invoke();
        }

        private void RestartLevel()
        {
            enableUpdate = false;

            gameData.levelObject.SetActive(false);
            gameData.levelObject = null;

            OnRestartEvent?.Invoke();
        }

        private void MovePlayer(int vertical, int horizontal)
        {
            Player activePlayer = _activePlayer;
            List<Box> activeBoxes = _activeBoxes;
            List<Key> activeKeys = _activeKeys;

            bool playerCanMove = true;

            if (vertical != 0)
            {
                int playerMoveX = activePlayer.gridX;
                int playerMoveY = activePlayer.gridY + vertical;

                // check impassables
                bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
                bool isPlayerCollidingLocks = IsCollidingLocks(playerMoveX, playerMoveY);
                playerCanMove = !isPlayerCollidingWalls && !isPlayerCollidingLocks;

                // Check pushables
                for (int boxIndex = 0; boxIndex < activeBoxes.Count; ++boxIndex)
                {
                    Box box = activeBoxes[boxIndex];

                    if (box.gridX == playerMoveX && box.gridY == playerMoveY)
                    {
                        int boxMoveX = box.gridX;
                        int boxMoveY = box.gridY + vertical;

                        bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                        bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);
                        bool isBoxCollidingKey = IsCollidingKeys(boxMoveX, boxMoveY);
                        bool isBoxCollidingLocks = IsCollidingLocks(boxMoveX, boxMoveY);

                        playerCanMove = !isBoxCollidingWall && !isBoxCollidingBox && !isBoxCollidingKey && !isBoxCollidingLocks;

                        if (playerCanMove)
                        {
                            activePlayer.target = box;
                        }
                    }
                }

                for (int keyIndex = 0; keyIndex < activeKeys.Count; ++keyIndex)
                {
                    Key key = activeKeys[keyIndex];

                    if (key.gridX == playerMoveX && key.gridY == playerMoveY)
                    {
                        int keyMoveX = key.gridX;
                        int keyMoveY = key.gridY + vertical;

                        bool isKeyCollidingWall = IsCollidingWalls(keyMoveX, keyMoveY);
                        bool isKeyCollidingBox = IsCollidingBoxes(keyMoveX, keyMoveY);
                        bool isKeyCollidingKey = IsCollidingKeys(keyMoveX, keyMoveY);

                        playerCanMove = !isKeyCollidingWall && !isKeyCollidingBox && !isKeyCollidingKey;

                        if (playerCanMove)
                        {
                            activePlayer.target = key;
                        }
                    }
                }
            }

            if (horizontal != 0)
            {
                int playerMoveX = activePlayer.gridX + horizontal;
                int playerMoveY = activePlayer.gridY;

                // check impassables
                bool isPlayerCollidingWalls = IsCollidingWalls(playerMoveX, playerMoveY);
                bool isPlayerCollidingLocks = IsCollidingLocks(playerMoveX, playerMoveY);
                playerCanMove = !isPlayerCollidingWalls && !isPlayerCollidingLocks;

                // Check pushables
                for (int boxIndex = 0; boxIndex < activeBoxes.Count; ++boxIndex)
                {
                    Box box = activeBoxes[boxIndex];

                    if (box.gridX == playerMoveX && box.gridY == playerMoveY)
                    {
                        int boxMoveX = box.gridX + horizontal;
                        int boxMoveY = box.gridY;

                        bool isBoxCollidingWall = IsCollidingWalls(boxMoveX, boxMoveY);
                        bool isBoxCollidingBox = IsCollidingBoxes(boxMoveX, boxMoveY);
                        bool isBoxCollidingKey = IsCollidingKeys(boxMoveX, boxMoveY);
                        bool isBoxCollidingLocks = IsCollidingLocks(boxMoveX, boxMoveY);

                        playerCanMove = !isBoxCollidingWall && !isBoxCollidingBox && !isBoxCollidingKey && !isBoxCollidingLocks;

                        if (playerCanMove)
                        {
                            activePlayer.target = box;
                        }
                    }
                }

                for (int keyIndex = 0; keyIndex < activeKeys.Count; ++keyIndex)
                {
                    Key key = activeKeys[keyIndex];

                    if (key.gridX == playerMoveX && key.gridY == playerMoveY)
                    {
                        int keyMoveX = key.gridX + horizontal;
                        int keyMoveY = key.gridY;

                        bool isKeyCollidingWall = IsCollidingWalls(keyMoveX, keyMoveY);
                        bool isKeyCollidingBox = IsCollidingBoxes(keyMoveX, keyMoveY);
                        bool isKeyCollidingKey = IsCollidingKeys(keyMoveX, keyMoveY);

                        playerCanMove = !isKeyCollidingWall && !isKeyCollidingBox && !isKeyCollidingKey;

                        if (playerCanMove)
                        {
                            activePlayer.target = key;
                        }
                    }
                }
            }

            if (playerCanMove)
            {
                sfxPlayer.PlayMoveSound();

                float movePlayerX = horizontal != 0 ? horizontal * cellSize.x : 0;
                float movePlayerY = vertical != 0 ? vertical * cellSize.y : 0;

                activePlayer.Move(movePlayerX, movePlayerY);

                int moveGridX = horizontal != 0 ? horizontal : 0;
                int moveGridY = vertical != 0 ? vertical : 0;

                activePlayer.MoveGrid(moveGridX, moveGridY);

                if (activePlayer.target != null)
                {
                    Pushable playerTargetBox = activePlayer.target;

                    float moveTargetX = horizontal != 0 ? horizontal * cellSize.x : 0;
                    float moveTargetY = vertical != 0 ? vertical * cellSize.y : 0;

                    playerTargetBox.Move(moveTargetX, moveTargetY);

                    int gridTargetX = horizontal != 0 ? horizontal : 0;
                    int gridTargetY = vertical != 0 ? vertical : 0;

                    playerTargetBox.MoveGrid(gridTargetX, gridTargetY);

                    activePlayer.target = null;
                }
            }
        }

        private bool IsCollidingWalls(int gridX, int gridY)
        {
            bool output = false;
            List<Wall> activeWalls = _activeWalls;

            for (int wallIndex = 0; wallIndex < activeWalls.Count; ++wallIndex)
            {
                var wall = activeWalls[wallIndex];

                if (wall.gridX == gridX && wall.gridY == gridY)
                {
                    output = true;
                }
            }

            return output;
        }

        private bool IsCollidingBoxes(int gridX, int gridY)
        {
            bool output = false;
            List<Box> activeBoxes = _activeBoxes;

            for (int boxIndex = 0; boxIndex < activeBoxes.Count; ++boxIndex)
            {
                Box box = activeBoxes[boxIndex];

                if (box.gridX == gridX && box.gridY == gridY)
                {
                    output = true;
                }
            }

            return output;
        }

        private bool IsCollidingLocks(int gridX, int gridY)
        {
            bool output = false;
            List<Lock> activeLocks = _activeLocks;

            for (int lockIndex = 0; lockIndex < activeLocks.Count; ++lockIndex)
            {
                var _lock = activeLocks[lockIndex];

                if (_lock.gridX == gridX && _lock.gridY == gridY)
                {
                    output = true;
                }
            }

            return output;
        }

        private bool IsCollidingKeys(int gridX, int gridY)
        {
            bool output = false;
            List<Key> activeKeys = _activeKeys;

            for (int keyIndex = 0; keyIndex < activeKeys.Count; ++keyIndex)
            {
                Key key = activeKeys[keyIndex];


                if (key.gridX == gridX && key.gridY == gridY)
                {
                    output = true;
                }
            }

            return output;
        }
    }
}
