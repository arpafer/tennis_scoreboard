using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class GameNormal: Game
    {
        private PointNormal _servicePoints;
        private PointNormal _restPoints;


        internal GameNormal() : base()
        {
            this.init();
        }

        internal void init()
        {
            this._servicePoints = new PointNormal();
            this._restPoints = new PointNormal();
        }

        internal bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || this._servicePoints.isDeuceWinner();
        }        

        protected override void _addServicePoints()
        {
            this._servicePoints.add(this._restPoints);
        }

        protected override void _addRestPoints()
        {
            this._restPoints.add(this._servicePoints);
        }

        protected override bool _hasWinner()
        {
            return this._servicePoints.isDeuceWinner() || this._restPoints.isDeuceWinner() || this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints);
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
