using WebAPI.Models;

namespace WebAPI.GameLogic
{
    public class Game
    {
        private List<List<int>> board;
        private int size;
        private Random random;

        public Game(int size)
        {
            this.size = size;
            random = new Random();
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            board = new List<List<int>>();
            for (int row = 0; row < size; row++)
            {
                var rowList = new List<int>();
                for (int col = 0; col < size; col++)
                {
                    rowList.Add(0);
                }
                board.Add(rowList);
            }

            GenerateRandomTile();
            GenerateRandomTile();
        }

        public void GenerateRandomTile()
        {
            List<(int, int)> emptyCells = new List<(int, int)>();
            for (int i = 0; i < size; i++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (board[i][y] == 0)
                    {
                        emptyCells.Add((i, y));
                    }
                }
            }

            if (emptyCells.Count == 0)
            {
                return;
            }

            int index = random.Next(0, emptyCells.Count);
            var (row, col) = emptyCells[index];

            board[row][col] = random.Next(1, 3) * 2; 
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[row][col] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsWinningTilePresent()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[row][col] >= 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<List<int>> GetBoard()
        {
            GameBoard boardCopy = new GameBoard();
            var list = new List<List<int>>();

            for (int row = 0; row < size; row++)
            {
                var rowList = new List<int>();
                for (int col = 0; col < size; col++)
                {
                    rowList.Add(board[row][col]);
                }
                list.Add(rowList);
            }
            boardCopy.Board = list;

            return boardCopy.Board;
        }

        private bool CanMove()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int currentTile = board[row][col];

                    if (currentTile == 0)
                    {
                        return true;
                    }

                    if (col + 1 < size && currentTile == board[row][col + 1])
                    {
                        return true;
                    }
                    if (row + 1 < size && currentTile == board[row + 1][col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void MoveTilesRight()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = size - 2; col >= 0; col--)
                {
                    int currentTile = board[row][col];

                    if (currentTile == 0)
                    {
                        continue;
                    }

                    int mergeTargetCol = col + 1;
                    while (mergeTargetCol < size && board[row][mergeTargetCol] == 0)
                    {
                        mergeTargetCol++;
                    }

                    if (mergeTargetCol < size && board[row][mergeTargetCol] == currentTile)
                    {
                        board[row][mergeTargetCol] *= 2;
                        board[row][col] = 0;
                    }
                    else
                    {
                        board[row][col] = 0;
                        board[row][mergeTargetCol - 1] = currentTile;
                    }
                }
            }

            GenerateRandomTile();
        }

        public void MoveTilesLeft()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 1; col < size; col++)
                {
                    int currentTile = board[row][col];

                    if (currentTile == 0)
                    {
                        continue;
                    }

                    int mergeTargetCol = col - 1;
                    while (mergeTargetCol >= 0 && board[row][mergeTargetCol] == 0)
                    {
                        mergeTargetCol--;
                    }

                    if (mergeTargetCol >= 0 && board[row][mergeTargetCol] == currentTile)
                    {
                        board[row][mergeTargetCol] *= 2;
                        board[row][col] = 0;
                    }
                    else
                    {
                        board[row][col] = 0;
                        board[row][mergeTargetCol + 1] = currentTile;
                    }
                }
            }

            GenerateRandomTile();
        }

        public void MoveTilesUp()
        {
            for (int col = 0; col < size; col++)
            {
                for (int row = 1; row < size; row++)
                {
                    int currentTile = board[row][col];

                    if (currentTile == 0)
                    {
                        continue;
                    }

                    int mergeTargetRow = row - 1;
                    while (mergeTargetRow >= 0 && board[mergeTargetRow][col] == 0)
                    {
                        mergeTargetRow--;
                    }

                    if (mergeTargetRow >= 0 && board[mergeTargetRow][col] == currentTile)
                    {
                        board[mergeTargetRow][col] *= 2;
                        board[row][col] = 0;
                    }
                    else
                    {
                        board[row][col] = 0;
                        board[mergeTargetRow + 1][col] = currentTile;
                    }
                }
            }

            GenerateRandomTile();
        }

        public void MoveTilesDown()
        {
            for (int col = 0; col < size; col++)
            {
                for (int row = size - 2; row >= 0; row--)
                {
                    int currentTile = board[row][col];

                    if (currentTile == 0)
                    {
                        continue;
                    }

                    int mergeTargetRow = row + 1;
                    while (mergeTargetRow < size && board[mergeTargetRow][col] == 0)
                    {
                        mergeTargetRow++;
                    }

                    if (mergeTargetRow < size && board[mergeTargetRow][col] == currentTile)
                    {
                        board[mergeTargetRow][col] *= 2;
                        board[row][col] = 0;
                    }
                    else
                    {
                        board[row][col] = 0;
                        board[mergeTargetRow - 1][col] = currentTile;
                    }
                }
            }

            GenerateRandomTile();
        }


        public bool CheckWin()
        {
            return IsWinningTilePresent();
        }

        public bool CheckLose()
        {
            return IsBoardFull() && !CanMove();
        }
    }
}
