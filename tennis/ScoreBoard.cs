using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public class ScoreBoard: IScoreBoard
    {        
        private Match _match;       

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
                case EventType.TIEBREAK:
                    Console.WriteLine("TIEBREAK !!!\n"); break;
                case EventType.END_MATCH:
                    Console.WriteLine("END MATCH !!!\n"); 
                    break;
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
