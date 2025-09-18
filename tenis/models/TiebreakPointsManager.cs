using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal class TiebreakPointsManager: PointsManager
    {
        internal TiebreakPointsManager(): base() 
        {
            this._servicePoints = new PointsTiebreak();
            this._restPoints = new PointsTiebreak();
        }

        internal override void addRestPoint()
        {
            (this._restPoints as PointsTiebreak).add();
        }

        internal override void addServicePoint()
        {
            (this._servicePoints as PointsTiebreak).add();
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
