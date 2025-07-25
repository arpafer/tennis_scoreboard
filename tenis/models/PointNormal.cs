﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class PointNormal: Point
    {
        internal const int ZERO = 0;
        internal const int FIFTEEN = 1;
        internal const int THIRTY = 2;
        internal const int FORTY = 3;
        internal const int AD = 4;
        internal const int WIN = 5;        

        internal PointNormal()
        {
            this._currentPoint = ZERO;
        }        

        internal void add(PointNormal other)
        {
            switch (this._currentPoint)
            {
                case ZERO: this._currentPoint = FIFTEEN; break;
                case FIFTEEN: this._currentPoint = THIRTY; break;
                case THIRTY: this._currentPoint = FORTY; break;
                case FORTY:
                    if (other._currentPoint == AD)
                    {
                        this._currentPoint = other._currentPoint = FORTY;
                    } else 
                       this._currentPoint = AD; 
                    break;
                case AD:                     
                    this._currentPoint = WIN; 
                    break;
            }
        }

        internal bool isDeuceWinner()
        {
            return this._currentPoint == PointNormal.WIN;
        }

        internal bool hasAd()
        {
            return this._currentPoint == PointNormal.AD;
        }

        internal override bool hasWonTo(Point other)
        {
            return this._currentPoint == PointNormal.WIN || (this._currentPoint == PointNormal.AD && other.currentPoint < FORTY);
        }

        internal override string toString()
        {
            string result = "";
            switch (this._currentPoint)
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
