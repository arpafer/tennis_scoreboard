using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public class ScoreBoard
    {
        private static ScoreBoard _instance;
        private Match _match;

        private ScoreBoard()
        {
            _instance = null;
        }

        public static ScoreBoard instance()
        {
            if (_instance == null)
            {
                _instance = new ScoreBoard();
            }
            return _instance;
        }

        public void set(Match match)
        {
            this._match = match;
        }

        internal void show(bool hasLack = false)
        {
            Console.WriteLine(this._match.toString(hasLack));
        }
    }
}
