using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadris
{
    class Tetromino
    {
        public char[,] matrix;
        public int padding;
        public Tetromino(char[,] mat, int pad)
        {
            matrix = mat;
            padding = pad;
        }
    }
}
