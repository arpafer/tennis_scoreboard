using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class GameNormal: Game
    {      
        internal GameNormal(IScoreBoard scoreboard, int[] idPlayers) : base(scoreboard, idPlayers)
        {
            this.init();
        }

        internal void init()
        {
            this._servicePoint = new PointNormal();
            this._restPoint = new PointNormal();
        }

        internal override bool isWinnerService()
        {
            return this._servicePoint.hasWonTo(this._restPoint) || (this._servicePoint as PointNormal).isDeuceWinner();
        }        

        protected override void _addServicePoint()
        {
            (this._servicePoint as PointNormal).add(this._restPoint as PointNormal);
        }

        protected override void _addRestPoint()
        {
            (this._restPoint as PointNormal).add(this._servicePoint as PointNormal);
        }

        protected override bool _hasWinner()
        {
            return ((this._servicePoint as PointNormal).isDeuceWinner() || (this._restPoint as PointNormal).isDeuceWinner() || this._servicePoint.hasWonTo(this._restPoint) || this._restPoint.hasWonTo(this._servicePoint));
        }

        protected override string _servicePointsToString()
        {
            return this._servicePoint.toString();
        }

        protected override string _restPointsToString()
        {
            return this._restPoint.toString();
        }
       
    }
}
