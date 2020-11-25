using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        private const char PlayerSymbol = 'O';
        private const string Winner = "{0} IS WINNER";
        private const string Tie = "TIE";
        
        private static bool _playerTurn = true;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void CheckGameEnd()
        {
            var isThereWinner = false;
            
            if (A1.Text != "" && A1.Text == A2.Text && A2.Text == A3.Text 
                || B1.Text != "" && B1.Text == B2.Text && B2.Text == B3.Text
                || C1.Text != "" && C1.Text == C2.Text && C2.Text == C3.Text)
            {
                isThereWinner = true;
            }
            
            else if (A1.Text != "" && A1.Text == B1.Text && B1.Text == C1.Text
            || A2.Text != "" && A2.Text == B2.Text && B2.Text == C2.Text
            || A3.Text != "" && A3.Text == B3.Text && B3.Text == C3.Text)
            {
                isThereWinner = true;
            }
            
            else if (A1.Text != "" && A1.Text == B2.Text && B2.Text == C3.Text
            || A3.Text != "" && A3.Text == B2.Text && B2.Text == C1.Text)
            {
                isThereWinner = true;
            }

            if (isThereWinner)
            {
                ShowWinner();
                return;
            }
            
            foreach (Control control in Controls)
            {
                if (control is Button && control.Enabled) return;
            }
            ShowTie();
        }

        private void ShowWinner()
        {
            label.Text = _playerTurn ? string.Format(Winner, PlayerSymbol) : string.Format(Winner, Boty.Symbol);

            foreach (Control control in Controls)
            {
                control.Enabled = !(control is Button);
            }
        }

        private void ShowTie()
        {
            label.Text = Tie;

            foreach (Control control in Controls)
            {
                control.Enabled = !(control is Button);
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var button = (Button) sender;
            
            button.Text  = (_playerTurn ? PlayerSymbol : Boty.Symbol).ToString();
            
            button.Enabled = false;
            CheckGameEnd();
            _playerTurn = !_playerTurn;
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (label.Text == "") return;

            NewGame();
        }

        private void NewGame()
        {
            foreach (Control control in Controls)
            {
                control.Text = "";
                control.Enabled = true;
            }
        }
    }
}