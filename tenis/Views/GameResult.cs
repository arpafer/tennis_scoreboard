using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.Views
{
    internal class GameResult
    {
        private Match _match;

        internal GameResult(Match match)
        {
            this._match = match;
        }

        internal void interact()
        {
            if (this._match.isEndGame())
            {
                Console.WriteLine("GAME BALL !!!\n");
            }
            if (this._match.isEndSet())
            {
                Console.WriteLine("SET BALL !!!\n");
            }
            if (this._match.isEndTiebreak())
            {
                Console.WriteLine("TIEBREAK !!!\n");
            }
            if (this._match.isEndMatch())
            {
                Console.WriteLine("END MATCH !!!\n");
            }
        }
    }
}
