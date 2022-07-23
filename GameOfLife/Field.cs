using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Field
    {
        public int Size { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Cell[,] GameField { get; private set; }

        public void CreateMap()
        {
            SetFieldSize();
            CreateGameField();
            PrintMap();
        }
        private void SetFieldSize()
        {
            Console.WriteLine("Введите размер поля");
            Size = int.Parse(Console.ReadLine());
            Height = Size;
            Width = Size * 3;
            Console.Clear();
        }
        
        private void CreateGameField()
        {
            var random = new Random();
            GameField = new Cell[Height, Width];
            for (int column = 0; column < Height; column++)
            {
                for (int row = 0; row < Width; row++)
                {
                    
                    if (random.Next(0,11) == 1)
                        GameField[column, row] = new Cell(column, row, GameField);
                    else
                    {
                        GameField[column, row] = new Cell(column, row, GameField, false);
                    }
                    GameController.UpdateCellState += GameField[column, row].cell_UpdateCellState;
                    GameController.UpdateGeneration += GameField[column, row].cell_UpdateGeneration;
                }
            }
        }

        private void PrintMap()
        {
            for (int column = 0; column <= Height; column++)
            {
                for (int row = 0; row <= Width; row++)
                {
                    Console.SetCursorPosition(row, column);
                    if (row == Width)
                    {
                        Console.Write("+");
                    }
                    if (column == Height && row != Width)
                        Console.Write("+");
                    
                }
                Console.WriteLine();
            }
        }
    }
}
