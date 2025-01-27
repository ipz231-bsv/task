﻿namespace TicTacToe
{
    internal class Cell
    {
        public int X { get; }
        public int Y { get; }
        public char Symbol { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = ' ';
        }
    }
}
