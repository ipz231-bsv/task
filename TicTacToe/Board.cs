using System.Windows.Forms;

namespace TicTacToe
{
    internal class Board
    {
        public Button[,] Cells { get; set; }

        public Board(int size)
        {
            Cells = new Button[size, size];
            // Ініціалізація клітинок може бути тут
        }

        public bool IsFull()
        {
            foreach (Button cell in Cells)
            {
                if (cell != null && cell.Text == "")
                    return false;
            }
            return true;
        }

        public bool HasWinner()
        {
            // Перевірка, чи є переможець (цю частину треба дописати залежно від вашої логіки)
            return false;
        }
    }
}
