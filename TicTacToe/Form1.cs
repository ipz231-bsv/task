using System;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        bool turn = true; // true = X turn; false = O turn;
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
            if ((p1.Text == "Player 1") || (p2.Text == "Player 2"))
            {
                MessageBox.Show("You must specify the player' names before you can start! \n ");
            }
            else
            {
                Button b = (Button)sender;
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";

                turn = !turn;
                b.Enabled = false;
                turn_count++;

                label3.Focus();
                CheckForWinner();

            }
            if ((turn) && (against_computer))
            {
                computer_make_move();
            }
        }
        private void computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = look_for_win_or_block("O"); //look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if

            move.PerformClick();
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }

        private void CheckForWinner()
        {
            bool there_is_a_winner = false;

            // horizontal, vertical, and diagonal checks
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

            foreach (var line in lines)
            {
                if (line[0] == line[1] && line[1] == line[2] && !string.IsNullOrEmpty(line[0]))
                {
                    there_is_a_winner = true;
                    break;
                }
            }

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
    }
}