using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class Tiebreak: Game
    {        
        internal Tiebreak(EventListener scoreboard, int[] idPlayers) : base(scoreboard, idPlayers)
        {
            this._servicePoints = new PointTiebreak();
            this._restPoints = new PointTiebreak();
        }

        protected override void _addRestPoints()
        {
            (this._restPoints as PointTiebreak).add();
        }

        protected override void _addServicePoints()
        {
            (this._servicePoints as PointTiebreak).add();
        }

        protected override bool _hasWinner()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints);
        }

        protected override string _restPointsToString()
        {
            return this._restPoints.toString();
        }

        protected override string _servicePointsToString()
        {
            return this._servicePoints.toString();
        }

        internal override bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints);
        }
    }
}
