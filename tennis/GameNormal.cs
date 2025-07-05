using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class GameNormal: Game
    {      
        internal GameNormal(EventListener scoreboard, int[] idPlayers) : base(scoreboard, idPlayers)
        {
            this.init();
        }

        internal void init()
        {
            this._servicePoints = new PointNormal();
            this._restPoints = new PointNormal();
        }

        internal override bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || (this._servicePoints as PointNormal).isDeuceWinner();
        }        

        protected override void _addServicePoints()
        {
            (this._servicePoints as PointNormal).add(this._restPoints as PointNormal);
        }

        protected override void _addRestPoints()
        {
            (this._restPoints as PointNormal).add(this._servicePoints as PointNormal);
        }

        protected override bool _hasWinner()
        {
            return ((this._servicePoints as PointNormal).isDeuceWinner() || (this._restPoints as PointNormal).isDeuceWinner() || this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints));
        }

        protected override string _servicePointsToString()
        {
            return this._servicePoints.toString();
        }

        protected override string _restPointsToString()
        {
            return this._restPoints.toString();
        }
       
    }
}
