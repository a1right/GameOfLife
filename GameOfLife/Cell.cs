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

        public void UpdateForNextGeneration()
        {
            if (_neighbours == null)
                SetNeighbours();
                
            SetCellStateInNextGeneration();
        }
        
        public void ApplyNextState()
        {
            SetCellStateAndPrint();
        }

        private void SetCellStateAndPrint()
        {
            if (IsAliveInNewGeneration != IsAlive)
            {
                IsAlive = IsAliveInNewGeneration;
            }
        }
        private void SetCellStateInNextGeneration()
        {
            int aliveNeighboursCount = _neighbours.Count(x => x.IsAlive);
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
            int lastRow = _field.GetLength(1) - 1;
            int firstColumn = 0;
            int lastColumn = _field.GetLength(0) - 1;
            int firstRow = 0;

            yield return IsCellInBounds(Column + 1, Row) ? _field[Column + 1, Row] : _field[firstColumn, Row];
                
            yield return IsCellInBounds(Column + 1, Row + 1) ? _field[Column + 1, Row + 1] : _field[firstColumn, firstRow];

            yield return IsCellInBounds(Column, Row + 1) ? _field[Column, Row + 1] : _field[Column, firstRow];
                
            yield return IsCellInBounds(Column - 1, Row + 1) ? _field[Column - 1, Row + 1] : _field[lastColumn, firstRow];

            yield return IsCellInBounds(Column - 1, Row) ? _field[Column - 1, Row] : _field[lastColumn, Row];

            yield return IsCellInBounds(Column - 1, Row - 1) ? _field[Column - 1, Row - 1] : _field[lastColumn,lastRow];
                
            yield return IsCellInBounds(Column, Row - 1) ? _field[Column, Row - 1] : _field[Column, lastRow];

            yield return IsCellInBounds(Column + 1, Row - 1) ? _field[Column + 1, Row - 1] : _field[firstColumn, lastRow];
                
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
