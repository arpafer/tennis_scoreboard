using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal abstract class Point
    {
        protected int _currentPoint;

        internal Point()
        {
            this._currentPoint = 0;
        }

        internal int currentPoint { get { return this._currentPoint; } }

        internal abstract bool hasWonTo(Point other);

        internal abstract string toString();
    }
}
