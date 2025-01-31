﻿using System;
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
            Button move = look_for_win_or_block("O")
                          ?? look_for_win_or_block("X")
                          ?? look_for_corner()
                          ?? look_for_open_space();

            move?.PerformClick();
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

            
            Button[] corners = { A1, A3, C1, C3 };

            
            foreach (var corner in corners)
            {
                if (corner.Text == "O")
                {
                    
                    if (A3.Text == "") return A3;
                    if (C3.Text == "") return C3;
                    if (C1.Text == "") return C1;
                }
            }

            
            foreach (var corner in corners)
            {
                if (corner.Text == "") return corner;
            }

            return null;
        }


        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);

            
            Button[,] winPatterns = new Button[,]
            {
        { A1, A2, A3 }, { B1, B2, B3 }, { C1, C2, C3 },
        { A1, B1, C1 }, { A2, B2, C2 }, { A3, B3, C3 },
        { A1, B2, C3 }, { A3, B2, C1 } 
            };

            
            for (int i = 0; i < winPatterns.GetLength(0); i++)
            {
                Button[] line = { winPatterns[i, 0], winPatterns[i, 1], winPatterns[i, 2] };
                Button move = FindWinningMove(line, mark);
                if (move != null) return move;
            }
            return null;
        }

        
        private Button FindWinningMove(Button[] line, string mark)
        {
            int countMark = line.Count(b => b.Text == mark);
            int countEmpty = line.Count(b => b.Text == "");

            if (countMark == 2 && countEmpty == 1)
            {
                return line.First(b => b.Text == "");
            }
            return null;
        }

        private void CheckForWinner()
        {
            bool there_is_a_winner = false;

            // horizontal checks
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            // vertical checks
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                there_is_a_winner = true;

            // diagonal checks
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            if (there_is_a_winner)
            {
                DisableButtons();

                string winner = "";
                if (turn) // Changed condition
                {
                    winner = p1.Text; // Player 1 wins
                    x_win_count.Text = (int.Parse(x_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = p2.Text; // Computer wins
                    o_win_count.Text = (int.Parse(o_win_count.Text) + 1).ToString();
                }

                MessageBox.Show(winner + " Wins!", "Yay!");
                NewGame();
            }
            else
            {
                if (turn_count == 9)
                {
                    DisableButtons();
                    draw_count.Text = (int.Parse(draw_count.Text) + 1).ToString();
                    MessageBox.Show("Draw!", "Bummer!");
                    NewGame();
                }
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