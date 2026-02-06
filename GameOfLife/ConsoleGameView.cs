using System;

namespace GameOfLife
{
    /// <summary>
    /// Implementation of IGameView for console-based display
    /// </summary>
    public class ConsoleGameView : IGameView
    {
        private int _width;
        private int _height;

        public void InitializeDisplay()
        {
            Console.CursorVisible = false;
        }

        public void DisplayField(Cell[,] field)
        {
            _height = field.GetLength(0);
            _width = field.GetLength(1);

            // Clear the screen before drawing
            Console.Clear();

            // Draw the border
            DrawBorder();

            // Draw the cells
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    var cell = field[row, col];
                    char symbol = cell.IsAlive ? (char)CellStateSymbols.CellIsAlive : (char)CellStateSymbols.CellIsDead;
                    
                    // Only draw inside the borders
                    Console.SetCursorPosition(col + 1, row + 1);
                    Console.Write(symbol);
                }
            }
        }

        public void DisplayGeneration(int generation)
        {
            Console.SetCursorPosition(0, _height + 2);
            Console.Write($"Generation: {generation}");
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void WaitForInput()
        {
            Console.SetCursorPosition(0, _height + 4);
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        public void DisplayGameOver()
        {
            Console.SetCursorPosition(0, _height + 4);
            Console.Write("Game Over!");
        }

        private void DrawBorder()
        {
            // Top border
            Console.SetCursorPosition(0, 0);
            Console.Write('+');
            for (int i = 0; i < _width; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');

            // Side borders and content
            for (int row = 0; row < _height; row++)
            {
                Console.SetCursorPosition(0, row + 1);
                Console.Write('|');
                
                Console.SetCursorPosition(_width + 1, row + 1);
                Console.Write('|');
            }

            // Bottom border
            Console.SetCursorPosition(0, _height + 1);
            Console.Write('+');
            for (int i = 0; i < _width; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');
        }
    }
}