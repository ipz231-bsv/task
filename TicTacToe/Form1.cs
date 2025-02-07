using System;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        bool turn = true;
        bool against_computer = false;
        int turn_count = 0;

        public TicTacToe()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By Butvinskyi Serhii", "Tic Tac Toe About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            ArePlayerNamesSet(sender);
            if ((turn) && (against_computer))
            {
                computer_make_move();
            }
        }

        private void ArePlayerNamesSet(object sender)
        {
            if ((p1.Text == "Player 1") || (p2.Text == "Player 2"))
            {
                MessageBox.Show("You must specify the player' names before you can start! \n ");
            }
            else
            {
                Button b = getClickedButton(sender);
                getCurrentPlayerSymbol(b);
                SwitchTurn(b);

                label3.Focus();
                CheckForWinner();
            }
        }

        private void computer_make_move()
        {
            Button move = null;
            move = CheckWinningMove();
            move = CheckBlockingMove(move);

            move.PerformClick();
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            return FindEmptyButton(ref b);
        }

        private Button look_for_corner()
        {
            Button[] corners = { A1, A3, C1, C3 };
            return FindStrategicCornerMove(corners);
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);

            Button[] buttons = { A1, A2, A3, B1, B2, B3, C1, C2, C3 };
            int[][] winPatterns = {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };
            return FindWinningMoveForMark(mark, buttons, winPatterns);
        }

        private void CheckForWinner()
        {
            bool there_is_a_winner = false;

            string[][] lines = new string[][]
            {
                new string[] { A1.Text, A2.Text, A3.Text },
                new string[] { B1.Text, B2.Text, B3.Text },
                new string[] { C1.Text, C2.Text, C3.Text },
                new string[] { A1.Text, B1.Text, C1.Text },
                new string[] { A2.Text, B2.Text, C2.Text },
                new string[] { A3.Text, B3.Text, C3.Text },
                new string[] { A1.Text, B2.Text, C3.Text },
                new string[] { A3.Text, B2.Text, C1.Text }
            };
            there_is_a_winner = CheckLineForWin(there_is_a_winner, lines);
            FinalizeGame(there_is_a_winner);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            turn = true;
            turn_count = 0;

            foreach (Control c in Controls)
            {
                try
                {
                    Button b = c as Button;
                    if (b != null)
                    {
                        b.Enabled = true;
                        b.Text = "";
                    }
                }
                catch { }
            }
        }

        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            }
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text = "";
            }
        }
        private void resetWinCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";
        }


        private void TicTacToe_Load(object sender, EventArgs e)
        {
            SetPlayerDefaultsToolStripMenuItem.PerformClick();
        }
        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text.ToUpper() == "COMPUTER")
                against_computer = true;
            else
                against_computer = false;
        }
        private void SetPlayerDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1.Text = "I";
            p2.Text = "Computer";
        }

        private void SwitchTurn(Button b)
        {
            turn = !turn;
            b.Enabled = false;
            turn_count++;
        }

        private void getCurrentPlayerSymbol(Button b)
        {
            if (turn)
                b.Text = "X";
            else
                b.Text = "O";
        }

        private static Button getClickedButton(object sender)
        {
            return (Button)sender;
        }

        private Button CheckBlockingMove(Button move)
        {
            if (move == null)
            {
                move = look_for_win_or_block("X");
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }
                }
            }

            return move;
        }

        private Button CheckWinningMove()
        {
            return look_for_win_or_block("O");
        }

        private Button FindEmptyButton(ref Button b)
        {
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }

            return null;
        }

        private static Button FindStrategicCornerMove(Button[] corners)
        {
            foreach (var corner in corners)
            {
                if (corner.Text == "O")
                {
                    foreach (var neighbor in corners)
                    {
                        if (neighbor != corner && neighbor.Text == "")
                        {
                            return neighbor;
                        }
                    }
                }
            }

            foreach (var corner in corners)
            {
                if (corner.Text == "")
                {
                    return corner;
                }
            }

            return null;
        }

        private static Button FindWinningMoveForMark(string mark, Button[] buttons, int[][] winPatterns)
        {
            foreach (var pattern in winPatterns)
            {
                Button first = buttons[pattern[0]];
                Button second = buttons[pattern[1]];
                Button third = buttons[pattern[2]];

                if (first.Text == mark && second.Text == mark && third.Text == "")
                    return third;
                if (second.Text == mark && third.Text == mark && first.Text == "")
                    return first;
                if (first.Text == mark && third.Text == mark && second.Text == "")
                    return second;
            }

            return null;
        }

        private void FinalizeGame(bool there_is_a_winner)
        {
            if (there_is_a_winner)
            {
                DisableButtons();

                string winner = turn ? p1.Text : p2.Text;
                if (turn)
                {
                    x_win_count.Text = (int.Parse(x_win_count.Text) + 1).ToString();
                }
                else
                {
                    o_win_count.Text = (int.Parse(o_win_count.Text) + 1).ToString();
                }

                MessageBox.Show(winner + " Wins!", "Yay!");
                NewGame();
            }
            else if (turn_count == 9)
            {
                DisableButtons();
                draw_count.Text = (int.Parse(draw_count.Text) + 1).ToString();
                MessageBox.Show("Draw!", "Bummer!");
                NewGame();
            }
        }

        private static bool CheckLineForWin(bool there_is_a_winner, string[][] lines)
        {
            foreach (var line in lines)
            {
                if (line[0] == line[1] && line[1] == line[2] && !string.IsNullOrEmpty(line[0]))
                {
                    there_is_a_winner = true;
                    break;
                }
            }

            return there_is_a_winner;
        }

        private void DisableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = c as Button;
                    if (b != null)
                    {
                        b.Enabled = false;
                    }
                }
            }
            catch { }
        }
    }
}