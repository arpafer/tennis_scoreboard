using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class PointTiebreak
    {
        private int _currentPoint;

        internal PointTiebreak()
        {
            this._currentPoint = 0;
        }

        internal void add()
        {
            this._currentPoint++;
        }

        internal bool hasWonTo(PointTiebreak other)
        {
            return (this._currentPoint - other._currentPoint) >= 2;
        }

        internal string toString()
        {
            return this._currentPoint.ToString();
        }
    }
}
