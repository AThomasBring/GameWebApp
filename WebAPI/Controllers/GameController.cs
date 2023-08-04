using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameLogic.GameLogic gameLogic;

        public GameController(GameLogic.GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        [HttpGet]
        public ActionResult<List<List<int>>> GetGameState()
        {
            return gameLogic.GetGameState();
        }

        [HttpPost("start")]
        public IActionResult StartNewGame()
        {
            gameLogic.StartNewGame();
            return Ok();
        }

        [HttpPost("move")]
        public IActionResult Move([FromBody] JObject data)
        {
            string direction = data["direction"].ToString();
            switch (direction.ToLower())
            {
                case "left":
                    gameLogic.MoveTilesLeft();
                    break;
                case "right":
                    gameLogic.MoveTilesRight();
                    break;
                case "up":
                    gameLogic.MoveTilesUp();
                    break;
                case "down":
                    gameLogic.MoveTilesDown();
                    break;
                default:
                    return BadRequest("Invalid move direction.");
            }

            if (gameLogic.CheckWin())
            {
                return Ok("You won!");
            }

            if (gameLogic.CheckLose())
            {
                return Ok("Game over!");
            }

            return Ok(gameLogic.GetGameState());
        }
    }
}
