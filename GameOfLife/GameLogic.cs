using System;

namespace GameOfLife
{
    /// <summary>
    /// Implementation of IGameLogic for Conway's Game of Life logic
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private Cell[,] _gameField;
        private int _size;
        private int _generation = 0;
        private bool _isRunning = false;

        public void InitializeGame(int size)
        {
            _size = size;
            CreateGameField();
        }

        public void UpdateGeneration()
        {
            if (_gameField == null) return;

            // Update all cells to calculate next state
            for (int row = 0; row < _gameField.GetLength(0); row++)
            {
                for (int col = 0; col < _gameField.GetLength(1); col++)
                {
                    _gameField[row, col].UpdateForNextGeneration();
                }
            }

            // Apply the next state to all cells
            for (int row = 0; row < _gameField.GetLength(0); row++)
            {
                for (int col = 0; col < _gameField.GetLength(1); col++)
                {
                    _gameField[row, col].ApplyNextState();
                }
            }

            _generation++;
        }

        public Cell[,] GetCurrentField()
        {
            return _gameField;
        }

        public int GetCurrentGeneration()
        {
            return _generation;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void StopGame()
        {
            _isRunning = false;
        }

        public void StartGame()
        {
            _isRunning = true;
        }

        private void CreateGameField()
        {
            var random = new Random();
            int height = _size;
            int width = _size * 3;

            _gameField = new Cell[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    bool isAlive = random.Next(0, 11) == 1; // 10% chance of being alive
                    _gameField[row, col] = new Cell(row, col, _gameField, isAlive);
                }
            }
        }
    }
}