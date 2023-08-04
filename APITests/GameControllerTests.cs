using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net;
using WebAPI.Controllers;
using WebAPI.GameLogic;
using Xunit;

namespace APITests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void StartNewGame_ReturnsOkResult()
        {
            //Arrange
            var gameLogicMock = new Mock<GameLogic>();
            var controller = new GameController(gameLogicMock.Object);
            // Act
            var result = controller.StartNewGame();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Move_WithValidDirection_ReturnsOkResult()
        {
            var gameLogicMock = new Mock<GameLogic>();
            var controller = new GameController(gameLogicMock.Object);
            var data = new JObject { { "direction", "left" } };

            var result = controller.Move(data);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Move_WithInvalidDirection_ReturnsBadRequestResult()
        {
            var gameLogicMock = new Mock<GameLogic>();
            var controller = new GameController(gameLogicMock.Object);
            var data = new JObject { { "direction", "invalid" } };

            var result = controller.Move(data);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}