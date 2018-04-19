using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadris
{
    class Tetromino
    {
        // A tetromino is a shape made out of 4 connected squares or blocks
        // In this class, we keep all possible (up to 4) tetrominos orientations as a matrix and a integer for padding.
        // We also keep it's X and Y coordiantes (which are mostly for simulation cases)
        protected List<TetrominoOrientation> orientations = new List<TetrominoOrientation>();
        protected int phase = 0; // Phase refers to the currently selected orientation's position in the List of TetrominoOrientations.
        protected int x = 4, y = 0;

        public TetrominoOrientation CurrentOrientation
        {
            get { return orientations[phase]; }
        }

        public List<TetrominoOrientation> Orientations
        {
            get { return orientations; }
        }

        public int PhaseCount
        {
            get { return orientations.Count; }
        }

        public int Phase
        {
            get { return phase; }
            set { phase = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public static Tetromino Copy(Tetromino source)
        {
            Tetromino copy = new Tetromino();
            copy.phase = source.phase;
            copy.x = source.x;
            copy.y = source.y;

            foreach (var item in source.orientations)
            {
                copy.orientations.Add(item);
            }
            return copy;
        }
    }
}
