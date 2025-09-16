using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal class NormalPointsPair: PointsPair
    {       
        internal NormalPointsPair(): base()
        {
            this._servicePoints = new PointNormal();
            this._restPoints = new PointNormal();
        }       

        internal override bool isWinnerService()
        {
            return this._servicePoints.hasWonTo(this._restPoints) || (this._servicePoints as PointNormal).isDeuceWinner();
        }

        internal override void addServicePoint()
        {
            (this._servicePoints as PointNormal).add(this._restPoints as PointNormal);
        }

        internal override void addRestPoint()
        {
            (this._restPoints as PointNormal).add(this._servicePoints as PointNormal);
        }

        internal override bool hasWinner()
        {
            return ((this._servicePoints as PointNormal).isDeuceWinner() || (this._restPoints as PointNormal).isDeuceWinner() || this._servicePoints.hasWonTo(this._restPoints) || this._restPoints.hasWonTo(this._servicePoints));
        }       
    }
}
