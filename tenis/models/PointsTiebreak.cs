using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class PointsTiebreak: Points
    {
        internal PointsTiebreak() 
        {
            this._currentPoints = 0;
        }

        internal void add()
        {
            this._currentPoints++;
        }

        internal override bool hasWonTo(Points other)
        {
            return (this._currentPoints - other.currentPoints) >= 2 && this._currentPoints > 6;
        }

        internal override string toString()
        {
            return this._currentPoints.ToString();
        }
    }
}
