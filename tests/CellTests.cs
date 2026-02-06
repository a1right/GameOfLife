using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class CellTests
    {
        [Fact]
        public void Constructor_SetsInitialValues()
        {
            // Arrange
            var field = new Cell[10, 10];
            int row = 5;
            int col = 3;
            bool isAlive = true;

            // Act
            var cell = new Cell(row, col, field, isAlive);

            // Assert
            Assert.Equal(row, cell.Row);
            Assert.Equal(col, cell.Column);
            Assert.True(cell.IsAlive);
            Assert.True(cell.IsAliveInNewGeneration);
        }

        [Fact]
        public void Constructor_DefaultsToDead()
        {
            // Arrange
            var field = new Cell[10, 10];
            int row = 5;
            int col = 3;

            // Act
            var cell = new Cell(row, col, field);

            // Assert
            Assert.Equal(row, cell.Row);
            Assert.Equal(col, cell.Column);
            Assert.False(cell.IsAlive);
            Assert.False(cell.IsAliveInNewGeneration);
        }

        [Fact]
        public void UpdateForNextGeneration_AppliesRulesCorrectly()
        {
            // Arrange
            var field = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = new Cell(i, j, field, false);
                }
            }

            // Create a center cell that is alive
            var centerCell = new Cell(1, 1, field, true);
            field[1, 1] = centerCell;

            // Make some neighbors alive to test rules
            field[0, 0] = new Cell(0, 0, field, true);
            field[0, 1] = new Cell(0, 1, field, true);
            field[0, 2] = new Cell(0, 2, field, true);

            // Act
            centerCell.UpdateForNextGeneration();

            // Assert
            // Center cell was alive with 3 neighbors, so it should stay alive
            Assert.Equal(true, centerCell.IsAliveInNewGeneration);
        }

        [Fact]
        public void ApplyNextState_UpdatesIsAlive()
        {
            // Arrange
            var field = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = new Cell(i, j, field, false);
                }
            }

            var cell = new Cell(1, 1, field, true);
            cell.UpdateForNextGeneration(); // This will set IsAliveInNewGeneration based on neighbors

            // Simulate that the cell should die (manually changing IsAliveInNewGeneration for test)
            typeof(Cell)
                .GetField("IsAliveInNewGeneration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(cell, false);

            bool initialAliveState = cell.IsAlive;

            // Act
            cell.ApplyNextState();

            // Assert
            Assert.NotEqual(initialAliveState, cell.IsAlive);
        }

        [Fact]
        public void UpdateForNextGeneration_DoesNotChangeStateIfSame()
        {
            // Arrange
            var field = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = new Cell(i, j, field, false);
                }
            }

            var cell = new Cell(1, 1, field, true);

            // Manually set IsAliveInNewGeneration to same as IsAlive
            typeof(Cell)
                .GetField("IsAliveInNewGeneration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(cell, true);

            bool initialAliveState = cell.IsAlive;

            // Act
            cell.UpdateForNextGeneration();
            cell.ApplyNextState();

            // Assert
            Assert.Equal(initialAliveState, cell.IsAlive);
        }
    }
}