using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal abstract class PointsPair
    {
        protected Point _servicePoint;
        protected Point _restPoint;

        internal abstract bool isWinnerService();
        internal abstract void addServicePoint();
        internal abstract void addRestPoint();
        internal abstract bool hasWinner();


        internal string toStringServicePoints()
        {
            return this._servicePoint.toString();
        }

        internal string toStringRestPoints()
        {
            return this._restPoint.toString();
        }
    }
}
