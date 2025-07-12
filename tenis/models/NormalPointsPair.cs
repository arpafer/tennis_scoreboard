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
            this._servicePoint = new PointNormal();
            this._restPoint = new PointNormal();
        }       

        internal override bool isWinnerService()
        {
            return this._servicePoint.hasWonTo(this._restPoint) || (this._servicePoint as PointNormal).isDeuceWinner();
        }

        internal override void addServicePoint()
        {
            (this._servicePoint as PointNormal).add(this._restPoint as PointNormal);
        }

        internal override void addRestPoint()
        {
            (this._restPoint as PointNormal).add(this._servicePoint as PointNormal);
        }

        internal override bool hasWinner()
        {
            return ((this._servicePoint as PointNormal).isDeuceWinner() || (this._restPoint as PointNormal).isDeuceWinner() || this._servicePoint.hasWonTo(this._restPoint) || this._restPoint.hasWonTo(this._servicePoint));
        }       
    }
}
