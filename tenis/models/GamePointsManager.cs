using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal class GamePointsManager: PointsManager
    {       
        internal GamePointsManager(): base()
        {
            this._servicePoints = new PointsGame();
            this._restPoints = new PointsGame();
        }       

        internal override bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || (this._servicePoints as PointsGame).isDeuceWinner();
        }

        internal override void addServicePoint()
        {
            (this._servicePoints as PointsGame).add(this._restPoints as PointsGame);
        }

        internal override void addRestPoint()
        {
            (this._restPoints as PointsGame).add(this._servicePoints as PointsGame);
        }

        internal override bool hasWinner()
        {
            return ((this._servicePoints as PointsGame).isDeuceWinner() || (this._restPoints as PointsGame).isDeuceWinner() || this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints));
        }       
    }
}
