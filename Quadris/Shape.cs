using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadris
{
    class Shape
    {
        public List<Tetromino> phases = new List<Tetromino>();
        public int phaseCount;
        public int phase = 0;
        public int x = 4, y = 0;

        public static Shape Copy(Shape source)
        {
            Shape copy = new Shape();
            copy.phaseCount = source.phaseCount;
            copy.phase = source.phase;
            copy.x = source.x;
            copy.y = source.y;

            foreach (var item in source.phases)
            {
                copy.phases.Add(item);
            }
            return copy;
        }
    }
}
