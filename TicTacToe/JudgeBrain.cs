using System.Collections.Generic;
using System.Linq;

namespace UI
{
    public class JudgeBrain
    {
        public string CheckWinner(List<string> board)
        {
            var winner = "";

            for (var i = 0; i < 9; i+=3)
            {
                if (Equals3(board[i], board[i + 1], board[i + 2]))
                {
                    winner = board[i];
                }
            }
            
            for (var i = 0; i < 3; i++)
            {
                if (Equals3(board[i], board[i + 3], board[i + 6]))
                {
                    winner = board[i];
                }
            }
            
            if (Equals3(board[0], board[4], board[8]))
            {
                winner = board[0];
            }
            if (Equals3(board[2], board[4], board[6]))
            {
                winner = board[2];
            }

            if (board.All(el => el != "") && winner == "")
            {
                return Form1.Tie;
            }
            return winner;
        }

        private bool Equals3(string a, string b, string c)
        {
            return a != "" && a == b && b == c;
        }
    }
}