using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public class ScoreBoard: EventListener
    {
        // private static ScoreBoard _instance;
        private Match _match;

        public ScoreBoard()
        {
         //   _instance = null;
        }

  /*      public static ScoreBoard instance()
        {
            if (_instance == null)
            {
                _instance = new ScoreBoard();
            }
            return _instance;
        }  */

        public void set(Match match)
        {
            this._match = match;
        }

        public void update(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.LACK_OF_SERVICE:
                    this._show(true); break;
                case EventType.END_GAME:
                    Console.WriteLine("GAME BALL !!!\n"); break;
                case EventType.END_SET:
                    Console.WriteLine("SET BALL !!!\n"); break;
                default:
                    this._show(); break;
            }                
        }

        private void _show(bool hasLack = false)
        {
            Console.WriteLine(this._match.toString(hasLack));
        }
    }
}
