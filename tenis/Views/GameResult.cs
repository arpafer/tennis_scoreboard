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
            if (this._match.isFinishedCurrentGame())
            {
                Console.WriteLine("GAME BALL !!!\n");
            }
            if (this._match.isFinishedCurrentSet())
            {
                Console.WriteLine("SET BALL !!!\n");
            }
            if (this._match.isFinishedCurrentTiebreak())
            {
                Console.WriteLine("TIEBREAK !!!\n");
            }
            if (this._match.isFinished())
            {
                Console.WriteLine("END MATCH !!!\n");
            }
        }
    }
}
