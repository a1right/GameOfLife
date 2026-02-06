using System;

namespace GameOfLife
{
    /// <summary>
    /// Interface for handling game logic operations
    /// </summary>
    public interface IGameLogic
    {
        void InitializeGame(int size);
        void UpdateGeneration();
        Cell[,] GetCurrentField();
        int GetCurrentGeneration();
        bool IsRunning();
        void StopGame();
        void StartGame();
    }
}