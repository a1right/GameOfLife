using System;
using System.Threading;

namespace GameOfLife
{
    public class GameController : IGameLogic
    {
        private readonly IGameView _view;
        private Cell[,] _field;
        private int _size;
        private int _generation = 0;
        private bool _isRunning = false;
        private readonly object _lockObject = new object();

        public GameController(IGameView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void InitializeGame(int size)
        {
            _size = size;
            CreateGameField();
            _view.InitializeDisplay();
        }

        public void StartGame()
        {
            _isRunning = true;
            _view.InitializeDisplay();
            
            while (_isRunning)
            {
                lock (_lockObject)
                {
                    UpdateGeneration();
                    _view.DisplayField(_field);
                    _view.DisplayGeneration(_generation);
                }
                
                Thread.Sleep(500); // Pause between generations
            }
        }

        public void UpdateGeneration()
        {
            if (_field == null) return;

            // Update all cells to calculate next state
            for (int row = 0; row < _field.GetLength(0); row++)
            {
                for (int col = 0; col < _field.GetLength(1); col++)
                {
                    _field[row, col].UpdateForNextGeneration();
                }
            }

            // Apply the next state to all cells
            for (int row = 0; row < _field.GetLength(0); row++)
            {
                for (int col = 0; col < _field.GetLength(1); col++)
                {
                    _field[row, col].ApplyNextState();
                }
            }

            _generation++;
        }

        public Cell[,] GetCurrentField()
        {
            lock (_lockObject)
            {
                return _field;
            }
        }

        public int GetCurrentGeneration()
        {
            lock (_lockObject)
            {
                return _generation;
            }
        }

        public bool IsRunning()
        {
            lock (_lockObject)
            {
                return _isRunning;
            }
        }

        public void StopGame()
        {
            lock (_lockObject)
            {
                _isRunning = false;
            }
        }

        private void CreateGameField()
        {
            var random = new Random();
            int height = _size;
            int width = _size * 3;

            _field = new Cell[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    bool isAlive = random.Next(0, 11) == 1; // 10% chance of being alive
                    _field[row, col] = new Cell(row, col, _field, isAlive);
                }
            }
        }
    }
}
