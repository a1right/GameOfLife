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
            if (generation < 2)
                SetNeighbours();
            SetCellStateInNextGeneration();
            SetCellStateAndPrint();
        }

        private void SetCellStateAndPrint()
        {
            if (!IsAliveInNewGeneration)
            {
                PrintCell();
                IsAlive = false;
            }

            if (IsAliveInNewGeneration)
            {
                PrintCell();
                IsAlive = true;
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
           if( aliveNeighboursCount == 3)
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
            if (IsCellInBounds(Column + 1, Row))
                yield return _field[Column + 1, Row];
            if (IsCellInBounds(Column + 1, Row + 1))
                yield return _field[Column + 1, Row + 1];
            if (IsCellInBounds(Column, Row + 1))
                yield return _field[Column, Row + 1];
            if (IsCellInBounds(Column - 1, Row + 1))
                yield return _field[Column - 1, Row + 1];
            if (IsCellInBounds(Column - 1, Row))
                yield return _field[Column - 1, Row];
            if (IsCellInBounds(Column - 1, Row - 1))
                yield return _field[Column - 1, Row - 1];
            if (IsCellInBounds(Column, Row - 1))
                yield return _field[Column, Row - 1];
            if (IsCellInBounds(Column + 1, Row - 1))
                yield return _field[Column + 1, Row - 1];

            //for (int columnDiff = -1; columnDiff <= 1; columnDiff++)
            //{
            //    for (int rowDiff = -1; rowDiff <= 1; rowDiff++)
            //    {
            //        if (columnDiff == rowDiff)
            //            continue;
            //        if (IsCellInBounds(Column + columnDiff, Row + rowDiff))
            //        {
            //            yield return _field[Column + columnDiff, Row + rowDiff];
            //        }
            //    }
            //}
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

        //private void CellDyingAnimation()
        //{

        //}
    }
}
