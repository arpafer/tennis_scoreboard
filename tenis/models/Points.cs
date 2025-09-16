using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal abstract class Points
    {
        protected int _currentPoints;

        internal Points()
        {
            this._currentPoints = 0;
        }

        internal int currentPoints { get { return this._currentPoints; } }

        internal abstract bool hasWonTo(Points other);

        internal abstract string toString();

        internal void switchPoints(Points other)
        {
            int _tmp = this._currentPoints;
            this._currentPoints = other._currentPoints;
            other._currentPoints = _tmp;
        }
    }
}
