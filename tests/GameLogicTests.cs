using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class GameLogicTests
    {
        [Fact]
        public void InitializeGame_SetsUpGameCorrectly()
        {
            // Arrange
            var gameLogic = new GameLogic();

            // Act
            gameLogic.InitializeGame(5);

            // Assert
            Assert.NotNull(gameLogic.GetCurrentField());
            Assert.Equal(0, gameLogic.GetCurrentGeneration());
            Assert.False(gameLogic.IsRunning());
        }

        [Fact]
        public void UpdateGeneration_IncrementsGeneration()
        {
            // Arrange
            var gameLogic = new GameLogic();
            gameLogic.InitializeGame(3);

            int initialGeneration = gameLogic.GetCurrentGeneration();

            // Act
            gameLogic.UpdateGeneration();

            // Assert
            Assert.Equal(initialGeneration + 1, gameLogic.GetCurrentGeneration());
        }

        [Fact]
        public void GetCurrentField_ReturnsField()
        {
            // Arrange
            var gameLogic = new GameLogic();
            gameLogic.InitializeGame(3);

            // Act
            var field = gameLogic.GetCurrentField();

            // Assert
            Assert.NotNull(field);
            Assert.Equal(3, field.GetLength(0));
            Assert.Equal(9, field.GetLength(1)); // 3 * 3
        }

        [Fact]
        public void GetCurrentGeneration_ReturnsGeneration()
        {
            // Arrange
            var gameLogic = new GameLogic();
            gameLogic.InitializeGame(3);

            // Act
            var generation = gameLogic.GetCurrentGeneration();

            // Assert
            Assert.Equal(0, generation);
        }

        [Fact]
        public void IsRunning_ReturnsRunningState()
        {
            // Arrange
            var gameLogic = new GameLogic();

            // Assert - initially should be false
            Assert.False(gameLogic.IsRunning());

            // Act
            typeof(GameLogic)
                .GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(gameLogic, true);

            // Assert - after manually setting to true
            Assert.True(gameLogic.IsRunning());
        }

        [Fact]
        public void StopGame_StopsGame()
        {
            // Arrange
            var gameLogic = new GameLogic();
            gameLogic.InitializeGame(3);
            
            // Manually start the game
            typeof(GameLogic)
                .GetField("_isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(gameLogic, true);

            // Act
            gameLogic.StopGame();

            // Assert
            Assert.False(gameLogic.IsRunning());
        }

        [Fact]
        public void StartGame_StartsGame()
        {
            // Arrange
            var gameLogic = new GameLogic();
            gameLogic.InitializeGame(3);

            // Act
            gameLogic.StartGame();

            // Assert
            Assert.True(gameLogic.IsRunning());
        }
    }
}