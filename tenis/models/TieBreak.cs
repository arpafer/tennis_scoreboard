using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.models
{
    internal class TieBreak : Game
    {
        private bool _firstService;
        private int _serviceTurns;

        internal TieBreak(Hashtable players) : base(players)
        {
            this._pointsPair = new TiebreakPointsPair();
            _firstService = true;
            this._serviceTurns = 0;
        }

        internal void setPoint(EventType eventType)
        {
            Console.WriteLine("Siguiente point tiebreak - turn: " + this._serviceTurns + " firstService: " + this._firstService);
            base.setPoint(eventType);
            if (this._firstService)
            {
                this._firstService = false;
                this._startServiceTurn();
            } else
            {
                this._serviceTurns++;
            }
            if (this._serviceTurns == 2)
            {
                this._startServiceTurn();
            }
        }

        private void _startServiceTurn()
        {
            this._switchService();
            this._serviceTurns = 0;
        }

        private void _switchService()
        {
            foreach (Player _player in this._players.Values)
            {
                _player.switchService();
            }

            (this._pointsPair as TiebreakPointsPair).switchPoints();
        }
    }
}
