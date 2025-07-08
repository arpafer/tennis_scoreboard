using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class Tiebreak: Game
    {        
        internal Tiebreak(IScoreBoard scoreboard, int[] idPlayers) : base(scoreboard, idPlayers)
        {
            this.init();
        }

        internal void init()
        {
            this._servicePoint = new PointTiebreak();
            this._restPoint = new PointTiebreak();
        }

        protected override void _addRestPoint()
        {
            (this._restPoint as PointTiebreak).add();
        }

        protected override void _addServicePoint()
        {
            (this._servicePoint as PointTiebreak).add();
        }

        protected override bool _hasWinner()
        {
            return this._servicePoint.hasWonTo(this._restPoint) || this._restPoint.hasWonTo(this._servicePoint);
        }

        protected override string _restPointsToString()
        {
            return this._restPoint.toString();
        }

        protected override string _servicePointsToString()
        {
            return this._servicePoint.toString();
        }

        internal override bool isWinnerService()
        {
            return this._servicePoint.hasWonTo(this._restPoint);
        }
    }
}
