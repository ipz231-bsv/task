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
            // Перевірка кожного кута, якщо він порожній
            if (A1.Text == "O" && A3.Text == "") return A3;
            if (A1.Text == "O" && C3.Text == "") return C3;
            if (A1.Text == "O" && C1.Text == "") return C1;

            if (A3.Text == "O" && A1.Text == "") return A1;
            if (A3.Text == "O" && C3.Text == "") return C3;
            if (A3.Text == "O" && C1.Text == "") return C1;

            if (C3.Text == "O" && A1.Text == "") return A1;
            if (C3.Text == "O" && A3.Text == "") return A3;
            if (C3.Text == "O" && C1.Text == "") return C1;

            if (C1.Text == "O" && A1.Text == "") return A1;
            if (C1.Text == "O" && A3.Text == "") return A3;
            if (C1.Text == "O" && C3.Text == "") return C3;

            // Якщо всі кути зайняті, повертаємо перший вільний
            if (A1.Text == "") return A1;
            if (A3.Text == "") return A3;
            if (C1.Text == "") return C1;
            if (C3.Text == "") return C3;

            return null;
        }


        private Button look_for_win_or_block(string mark)
        {
            // Горизонтальні перевірки
            Button result = CheckLine(A1, A2, A3, mark);
            if (result != null) return result;
            result = CheckLine(B1, B2, B3, mark);
            if (result != null) return result;
            result = CheckLine(C1, C2, C3, mark);
            if (result != null) return result;

            // Вертикальні перевірки
            result = CheckLine(A1, B1, C1, mark);
            if (result != null) return result;
            result = CheckLine(A2, B2, C2, mark);
            if (result != null) return result;
            result = CheckLine(A3, B3, C3, mark);
            if (result != null) return result;

            // Діагональні перевірки
            result = CheckLine(A1, B2, C3, mark);
            if (result != null) return result;
            result = CheckLine(A3, B2, C1, mark);
            if (result != null) return result;

            return null;
        }

        private Button CheckLine(Button b1, Button b2, Button b3, string mark)
        {
            if (b1.Text == mark && b2.Text == mark && b3.Text == "")
                return b3;
            if (b2.Text == mark && b3.Text == mark && b1.Text == "")
                return b1;
            if (b1.Text == mark && b3.Text == mark && b2.Text == "")
                return b2;

            return null;
        }

        private bool CheckHorizontal(Button b1, Button b2, Button b3)
        {
            return (b1.Text == b2.Text && b2.Text == b3.Text && !b1.Enabled);
        }

        private bool CheckVertical(Button b1, Button b2, Button b3)
        {
            return (b1.Text == b2.Text && b2.Text == b3.Text && !b1.Enabled);
        }

        private bool CheckDiagonal(Button b1, Button b2, Button b3)
        {
            return (b1.Text == b2.Text && b2.Text == b3.Text && !b1.Enabled);
        }

        private void CheckForWinner()
        {
            bool there_is_a_winner = false;

            // Check horizontal lines
            if (CheckHorizontal(A1, A2, A3)) there_is_a_winner = true;
            else if (CheckHorizontal(B1, B2, B3)) there_is_a_winner = true;
            else if (CheckHorizontal(C1, C2, C3)) there_is_a_winner = true;

            // Check vertical lines
            else if (CheckVertical(A1, B1, C1)) there_is_a_winner = true;
            else if (CheckVertical(A2, B2, C2)) there_is_a_winner = true;
            else if (CheckVertical(A3, B3, C3)) there_is_a_winner = true;

            // Check diagonals
            else if (CheckDiagonal(A1, B2, C3)) there_is_a_winner = true;
            else if (CheckDiagonal(A3, B2, C1)) there_is_a_winner = true;

            if (there_is_a_winner)
            {
                DisableButtons();
                string winner = turn ? p1.Text : p2.Text;
                if (turn) x_win_count.Text = (int.Parse(x_win_count.Text) + 1).ToString();
                else o_win_count.Text = (int.Parse(o_win_count.Text) + 1).ToString();
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