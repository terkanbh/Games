class GameState
{
    private byte?[,] _board = new byte?[6, 7];
    public byte Player { get; private set; } = 1;
    public byte Turn { get; private set; } = 1;

    public int PlayPiece(byte col)
    {
        if (col > 6) return -1;

        for (int row = 5; row >= 0; row--)
        {
            if (_board[row, col] == null)
            {
                _board[row, col] = Player;
                return row;
            }
        }

        return -1; // Column is full
    }

    public int? CheckForWin()
    {
        if (Turn == 42) return 0; // Tie

        var directions = new (int dRow, int dCol)[] { (0, 1) , ( 1, 0 ), ( 1, 1 ), ( 1, -1 ) };

        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (_board[row, col] == Player)
                {
                    foreach (var (dRow, dCol) in directions)
                    {
                        if (IsWinningLine(row, col, dRow, dCol))
                            return Player; // Return the winner (1 or 2)
                    }
                }
            }
        }

        return null; // No winner
    }

    public void ResetGame()
    {
        _board = new byte?[6, 7];
        Player = 1;
        Turn = 1;
    }

    public void NextTurn()
    {
        Player = (byte)(3 - Player); // Switch between 1 and 2
        Turn++;
    }

    private bool IsWinningLine(int row, int col, int dRow, int dCol)
    {
        for (int i = 1; i < 4; i++) // Check next 3 positions
        {
            int newRow = row + i * dRow;
            int newCol = col + i * dCol;

            if (newRow < 0 || newRow >= 6 || newCol < 0 || newCol >= 7 || _board[newRow, newCol] != Player)
                return false;
        }
        return true;
    }
}