using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public const string PlayerSymbol = "O";
        public const string BotSymbol = "X"; 
        public const string Tie = "TIE";
        private const string Win = "{0} IS WINNER";
        
        private static bool _playerTurn = true;

        private List<string> _board = new List<string>
        {
            "", "", "", "", "", "", "", "", ""
        };

        private readonly Dictionary<int, Button> _buttons;
        private readonly JudgeBrain _judge;
        private readonly Boty _boty;

        public Form1()
        {
            InitializeComponent();
            label.Select();

            _judge = new JudgeBrain();
            _boty = new Boty();

            _buttons = new Dictionary<int, Button>
            {
                {0, A1}, {1, A2}, {2, A3}, 
                {3, B1}, {4, B2}, {5, B3}, 
                {6, C1}, {7, C2}, {8, C3}
            };
        }
        
        private void CheckGameEnd()
        {
            label.Text = _judge.CheckWinner(_board);

            if (label.Text == "") return;

            if (label.Text != Tie)
            {
                label.Text = string.Format(Win, label.Text);
            }

            foreach (Control control in Controls)
            {
                control.Enabled = !(control is Button);
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (!_playerTurn) return;
            
            var button = (Button) sender;
            var key = _buttons.First(el => el.Value == button).Key;

            _board[key] = PlayerSymbol;
            
            button.Text  = PlayerSymbol;
            button.Enabled = false;
            label.Select();
            
            CheckGameEnd();

            if (_board.All(el => el != "")) return;
            
            _playerTurn = !_playerTurn;
            GetBotMove();
        }

        private void GetBotMove()
        {
            var move = _boty.FindBestMove(_board);
            var button = _buttons[move];

            _board[move] = BotSymbol;
            
            button.Text  = BotSymbol;
            button.Enabled = false;
            
            CheckGameEnd();
            
            _playerTurn = !_playerTurn;
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (label.Text == "") return;

            _board = new List<string>
            {
                "", "", "", "", "", "", "", "", ""
            };
            
            foreach (Control control in Controls)
            {
                control.Text = "";
                control.Enabled = true;
            }
        }
    }
}