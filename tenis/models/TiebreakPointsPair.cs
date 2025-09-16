using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal class TiebreakPointsPair: PointsPair
    {
        internal TiebreakPointsPair(): base() 
        {
            this._servicePoints = new PointTiebreak();
            this._restPoints = new PointTiebreak();
        }

        internal override void addRestPoint()
        {
            (this._restPoints as PointTiebreak).add();
        }

        internal override void addServicePoint()
        {
            (this._servicePoints as PointTiebreak).add();
        }

        internal override bool hasWinner()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints);
        }       

        internal override bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints);
        }

        internal void switchPoints()
        {
            this._servicePoints.switchPoints(this._restPoints);
        }
    }
}
