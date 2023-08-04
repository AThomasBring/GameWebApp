namespace WebAPI.GameLogic
{
    public class GameLogic
    {
        private Game game;

        public GameLogic()
        {
            game = new Game(4);
        }

        public void StartNewGame()
        {
            game.InitializeBoard();
        }

        public virtual List<List<int>> GetGameState()
        {
            return game.GetBoard();
        }

        public void MoveTilesLeft()
        {
            game.MoveTilesLeft();
        }

        public void MoveTilesRight()
        {
            game.MoveTilesRight();
        }

        public void MoveTilesUp()
        {
            game.MoveTilesUp();
        }

        public void MoveTilesDown()
        {
            game.MoveTilesDown();
        }

        public bool CheckWin()
        {
            return game.CheckWin();
        }

        public bool CheckLose()
        {
            return game.CheckLose();
        }
    }
}
