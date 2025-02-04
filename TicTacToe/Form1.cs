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
            // Массив для кутів, що перевіряються
            Button[] corners = { A1, A3, C1, C3 };

            // Перевірка кожного кута
            foreach (var corner in corners)
            {
                // Якщо кут є "O" і сусідній кут вільний, повертаємо цей кут
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

            // Якщо всі кути зайняті, повертаємо перший вільний кут
            foreach (var corner in corners)
            {
                if (corner.Text == "")
                {
                    return corner;
                }
            }

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);

            // Горизонтальні перевірки
            Button[] buttons = { A1, A2, A3, B1, B2, B3, C1, C2, C3 };
            int[][] winPatterns = {
        new int[] { 0, 1, 2 }, // A1, A2, A3
        new int[] { 3, 4, 5 }, // B1, B2, B3
        new int[] { 6, 7, 8 }, // C1, C2, C3
        new int[] { 0, 3, 6 }, // A1, B1, C1
        new int[] { 1, 4, 7 }, // A2, B2, C2
        new int[] { 2, 5, 8 }, // A3, B3, C3
        new int[] { 0, 4, 8 }, // A1, B2, C3
        new int[] { 2, 4, 6 }  // A3, B2, C1
    };

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