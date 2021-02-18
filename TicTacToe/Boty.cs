using System;
using System.Collections.Generic;

namespace UI
{
    public class Boty
    {
        private readonly JudgeBrain _judge;
        private readonly Dictionary<string, int> _scores = new Dictionary<string, int>
        {
            {"X", 1},
            {"O", -1},
            {"TIE", 0}
        };

        public Boty()
        {
            _judge = new JudgeBrain();
        }

        public int FindBestMove(List<string> board)
        {
            var bestScore = int.MinValue;
            var move = 0;

            for (var i = 0; i < 9; i++)
            {
                if (board[i] != "") continue;
                
                board[i] = Form1.BotSymbol;
                var score = Minimax(board, 0, false);
                board[i] = "";
                    
                if (score <= bestScore) continue;
                    
                bestScore = score;
                move = i;
            }

            return move;
        }

        private int Minimax(List<string> board, int depth, bool isMaximizing)
        {
            var result = _judge.CheckWinner(board);

            if (result != "")
            {
                return _scores[result];
            }

            if (isMaximizing)
            {
                var bestScore = int.MinValue;

                for (var i = 0; i < 9; i++)
                {
                    if (board[i] != "") continue;

                    board[i] = Form1.BotSymbol;
                    var score = Minimax(board, depth + 1, false);
                    board[i] = "";

                    bestScore = Math.Max(bestScore, score);
                }

                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                
                for (var i = 0; i < 9; i++)
                {
                    if (board[i] != "") continue;

                    board[i] = Form1.PlayerSymbol;
                    var score = Minimax(board, depth + 1, true);
                    board[i] = "";

                    bestScore = Math.Min(bestScore, score);
                }
                
                return bestScore;
            }
        }
    }
}