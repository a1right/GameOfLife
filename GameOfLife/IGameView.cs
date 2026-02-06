using System;

namespace GameOfLife
{
    /// <summary>
    /// Interface for handling game display and user interaction
    /// </summary>
    public interface IGameView
    {
        void InitializeDisplay();
        void DisplayField(Cell[,] field);
        void DisplayGeneration(int generation);
        void ClearScreen();
        void WaitForInput();
        void DisplayGameOver();
    }
}