using System.Windows.Forms;

namespace TicTacToe
{
    internal class Board
    {
        public Button[,] Cells { get; set; }

        public Board(int size)
        {
            Cells = new Button[size, size];
        }

        public bool IsFull()
        {
            return CheckIfBoardIsFull();
        }

        public bool HasWinner()
        {
            return false;
        }

        private bool CheckIfBoardIsFull()
        {
            foreach (Button cell in Cells)
            {
                if (cell != null && cell.Text == "")
                    return false;
            }
            return true;
        }
    }
}
