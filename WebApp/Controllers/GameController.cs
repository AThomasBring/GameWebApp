using Microsoft.AspNetCore.Mvc;
using WebAPI.GameLogic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private ApiUrl url { get; set; }
        
        private readonly GameLogic gameLogic;

        public GameController(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var state = gameLogic.GetGameState();
            var model = new GameBoard() { Board = state };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewGame()
        {
            await StartNewGameAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Move(string direction)
        {
            if (!string.IsNullOrEmpty(direction))
            {
                await MakeMoveAsync(direction.ToLower());
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<List<int>>> GetGameStateAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url.Url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<List<int>>>();
                    return result;
                }
                else
                {
                    throw new Exception("Error retrieving game state: " + response.StatusCode);
                }
            }
        }

        private async Task StartNewGameAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync($"{url.Url}/start", null);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error starting new game: " + response.StatusCode);
                }
            }
        }

        private async Task MakeMoveAsync(string direction)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync($"{url.Url}/move", JsonContent.Create(direction));
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error making move: " + response.StatusCode);
                }
            }
        }
    }
}
