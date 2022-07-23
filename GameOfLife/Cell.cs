using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Cell
    {
        private  Cell[,] _field;
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsAliveInNewGeneration { get; private set; }

        private List<Cell> _neighbours;
        public Cell(int column, int row, Cell[,] field, bool isAlive = true)
        {
            Column = column;
            Row = row;
            IsAlive = isAlive;
            _field = field;
            IsAliveInNewGeneration = IsAlive;
        }

        public void cell_UpdateGeneration(object sender, GenerationEventArgs e)
        {
            DoLifeCycle(e.Generation);
        }
        public void DoLifeCycle(int generation)
        {
            if (generation < 1)
                SetNeighbours();
            SetCellStateAndPrint();
        }
        public void cell_UpdateCellState(object? sender, EventArgs e)
        {
            SetCellStateInNextGeneration();
        }

        private void SetCellStateAndPrint()
        {
            if (IsAliveInNewGeneration != IsAlive)
            {
                PrintCell();
                IsAlive = IsAliveInNewGeneration;
            }
        }
        private void SetCellStateInNextGeneration()
        {
            int aliveNeighboursCount = _neighbours.Where(x => x.IsAlive == true).Count();
            if (IsAlive)
            {
                if (aliveNeighboursCount < 2 || aliveNeighboursCount > 3 )
                {
                    IsAliveInNewGeneration = false;
                    return;
                }
            }
           if(!IsAlive && aliveNeighboursCount == 3)
            {
                IsAliveInNewGeneration = true;
                return;
            }
        }

        private void SetNeighbours()
        {
            _neighbours = GetNeighboursInBounds().Where(x => x != null).ToList();
        }

        private IEnumerable<Cell> GetNeighboursInBounds()
        {
            int fieldWidth = _field.GetLength(1);
            int fieldHeight = _field.GetLength(0);
            yield return IsCellInBounds(Column + 1, Row) ? _field[Column + 1, Row] : _field[0, Row];
                
            yield return IsCellInBounds(Column + 1, Row + 1) ? _field[Column + 1, Row + 1] : _field[0, 0];

            yield return IsCellInBounds(Column, Row + 1) ? _field[Column, Row + 1] : _field[Column, 0];
                
            yield return IsCellInBounds(Column - 1, Row + 1) ? _field[Column - 1, Row + 1] : _field[_field.GetLength(0) - 1, 0];

            yield return IsCellInBounds(Column - 1, Row) ? _field[Column - 1, Row] : _field[_field.GetLength(0) - 1, Row];

            yield return IsCellInBounds(Column - 1, Row - 1) ? _field[Column - 1, Row - 1] : _field[_field.GetLength(0) - 1, _field.GetLength(1) - 1];
                
            yield return IsCellInBounds(Column, Row - 1) ? _field[Column, Row - 1] : _field[Column, _field.GetLength(1) - 1];

            yield return IsCellInBounds(Column + 1, Row - 1) ? _field[Column + 1, Row - 1] : _field[0, _field.GetLength(1) - 1];
                
        }

        private bool IsCellInBounds(int column, int row)
        {
            if (column >= 0 && column < _field.GetLength(0) && row >= 0 && row < _field.GetLength(1))
                return true;
            return false;
        }

        private void PrintCell()
        {
            char currentState =  IsAliveInNewGeneration ? (char)CellStateSymbols.CellIsAlive : (char)CellStateSymbols.CellIsDead;
            Console.SetCursorPosition(Row, Column);
            Console.Write(currentState);
        }
    }
}
