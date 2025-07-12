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
            this._servicePoint = new PointTiebreak();
            this._restPoint = new PointTiebreak();
        }

        internal override void addRestPoint()
        {
            (this._restPoint as PointTiebreak).add();
        }

        internal override void addServicePoint()
        {
            (this._servicePoint as PointTiebreak).add();
        }

        internal override bool hasWinner()
        {
            return this._servicePoint.hasWonTo(this._restPoint) || this._restPoint.hasWonTo(this._servicePoint);
        }       

        internal override bool isWinnerService()
        {
            return this._servicePoint.hasWonTo(this._restPoint);
        }
    }
}
