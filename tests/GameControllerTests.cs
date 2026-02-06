using Moq;
using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class GameControllerTests
    {
        [Fact]
        public void Constructor_WithNullView_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GameController(null));
        }

        [Fact]
        public void InitializeGame_SetsUpGameCorrectly()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);

            // Act
            controller.InitializeGame(5);

            // Assert
            Assert.NotNull(controller.GetCurrentField());
            Assert.Equal(0, controller.GetCurrentGeneration());
            mockView.Verify(v => v.InitializeDisplay(), Times.Once);
        }

        [Fact]
        public void StartGame_StartsGameLoop()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);

            controller.InitializeGame(3);

            // Act
            var task = Task.Run(() => controller.StartGame());
            
            // Allow some time for execution
            Thread.Sleep(100);
            
            // Stop the game to prevent infinite loop
            controller.StopGame();
            
            // Wait for the task to complete
            Task.WaitAll(task);

            // Assert
            Assert.False(controller.IsRunning());
        }

        [Fact]
        public void UpdateGeneration_IncrementsGeneration()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);
            controller.InitializeGame(3);

            int initialGeneration = controller.GetCurrentGeneration();

            // Act
            controller.UpdateGeneration();

            // Assert
            Assert.Equal(initialGeneration + 1, controller.GetCurrentGeneration());
        }

        [Fact]
        public void GetCurrentField_ReturnsField()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);
            controller.InitializeGame(3);

            // Act
            var field = controller.GetCurrentField();

            // Assert
            Assert.NotNull(field);
            Assert.Equal(3, field.GetLength(0));
            Assert.Equal(9, field.GetLength(1)); // 3 * 3
        }

        [Fact]
        public void GetCurrentGeneration_ReturnsGeneration()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);
            controller.InitializeGame(3);

            // Act
            var generation = controller.GetCurrentGeneration();

            // Assert
            Assert.Equal(0, generation);
        }

        [Fact]
        public void IsRunning_ReturnsRunningState()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);

            // Act
            controller.StartGame();
            controller.StopGame();

            // Assert
            Assert.False(controller.IsRunning());
        }

        [Fact]
        public void StopGame_StopsGame()
        {
            // Arrange
            var mockView = new Mock<IGameView>();
            var controller = new GameController(mockView.Object);
            controller.InitializeGame(3);

            // Act
            controller.StartGame();
            controller.StopGame();

            // Assert
            Assert.False(controller.IsRunning());
        }
    }
}