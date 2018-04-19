using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadris
{
    class TetrominoOrientation
    {
        // TetrominoOrientation class keeps 1 instance of a Tetromino's orientation.
        // It also returns it's padding, which for the most part is there to determine how far right the right-most block is frome the source block.
        // Source block is [0,0], if it was a cube, which a 2x2 tetromino with only 1 orientation.
        // It's padding would be 1, because the right most block is at x=1, 1-0 = 1.
        // Or an esiear way to look at it would be, the matrixes width - 1, so the index of it's right most block.
        protected char[,] matrix;

        public TetrominoOrientation(char[,] mat)
        {
            matrix = mat;
        }

        public char[,] Matrix
        {
            get { return matrix; }
        }

        public int Padding
        {
            get { return matrix.GetLength(1) - 1; }
        }
    }
}
