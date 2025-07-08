using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class PointTiebreak: Point
    {
        internal PointTiebreak() 
        {
            this._currentPoint = 0;
        }

        internal void add()
        {
            this._currentPoint++;
        }

        internal override bool hasWonTo(Point other)
        {
            return (this._currentPoint - other.currentPoint) >= 2;
        }

        internal override string toString()
        {
            return this._currentPoint.ToString();
        }
    }
}
