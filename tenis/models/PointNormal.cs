using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class PointNormal: Points
    {
        internal const int ZERO = 0;
        internal const int FIFTEEN = 1;
        internal const int THIRTY = 2;
        internal const int FORTY = 3;
        internal const int AD = 4;
        internal const int WIN = 5;        

        internal PointNormal()
        {
            this._currentPoints = ZERO;
        }        

        internal void add(PointNormal other)
        {
            switch (this._currentPoints)
            {
                case ZERO: this._currentPoints = FIFTEEN; break;
                case FIFTEEN: this._currentPoints = THIRTY; break;
                case THIRTY: this._currentPoints = FORTY; break;
                case FORTY:
                    if (other._currentPoints == AD)
                    {
                        this._currentPoints = other._currentPoints = FORTY;
                    } else 
                       this._currentPoints = AD; 
                    break;
                case AD:                     
                    this._currentPoints = WIN; 
                    break;
            }
        }

        internal bool isDeuceWinner()
        {
            return this._currentPoints == PointNormal.WIN;
        }

        internal bool hasAd()
        {
            return this._currentPoints == PointNormal.AD;
        }

        internal override bool hasWonTo(Points other)
        {
            return this._currentPoints == PointNormal.WIN || (this._currentPoints == PointNormal.AD && other.currentPoints < FORTY);
        }

        internal override string toString()
        {
            string result = "";
            switch (this._currentPoints)
            {
                case ZERO: result = "0"; break;
                case FIFTEEN: result = "15"; break;
                case THIRTY: result = "30"; break;
                case FORTY: result = "40"; break;
                case AD: result = "AD"; break;
            }
            return result;
        }       
    }
}
