using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.GameLogic;

namespace WebApp.Views
{
    public class GameModel : PageModel
    {
        private readonly GameLogic gameLogic;

        public GameModel(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        public List<List<int>> GameState { get; set; }

        public void OnGet()
        {
            GameState = gameLogic.GetGameState();
        }
    }
}