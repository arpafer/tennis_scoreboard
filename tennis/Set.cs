using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class Set
    {        
        private List<GameNormal> _games;
        private bool _hasTiebreak;
        private int _serviceGames;
        private int _restGames;
        private bool _hasWinner;        

        internal Set()
        {
            this._hasTiebreak = false;
            this._games = new List<GameNormal>();
            this._serviceGames = 0;
            this._restGames = 0;
            this._hasWinner = false;
        }

        internal void play()
        {            
            do
            {                
                if (this._hasTiebreak)
                {
                    Tiebreak _tiebreak = new Tiebreak();
                    _tiebreak.play();
                    _hasWinner = this._hasTiebreakWinner(_tiebreak);
                } else
                {
                    GameNormal _game = new GameNormal();
                    _game.play();
                    this._games.Add(_game);
                    _hasWinner = this._hasSetWinner(_game);
                }
            } while (!_hasWinner);
            ScoreBoard.instance().show();
        }

        private bool _hasSetWinner(GameNormal game)
        {                                  
            if (game.isWinnerService())
                this._serviceGames++;
            else
                this._restGames++;

            if (this._serviceGames == 6 && this._restGames == this._serviceGames)
            {
                this._hasTiebreak = true;                
                return false;
            }
            if (Math.Abs(this._serviceGames - this._restGames) >= 2)
            {
                return true;
            }
            return false;
        }        

        private bool _hasServiceWinner()
        {
            return this._serviceGames - this._restGames >= 2;
        }

        private bool _hasTiebreakWinner(Tiebreak _tiebreak)
        {
            if (_tiebreak.isWinnerService())
                this._serviceGames++;
            else
                this._restGames++;

            if (Math.Abs(this._serviceGames - this._restGames) >= 2)
            {
                return true;
            }
            return false;
        }

        internal bool isFinished()
        {
            return this._hasWinner;
        }
     
        internal string toString(int id)
        {
            string result = "";
            Player _player = PlayersManager.instance().getPlayerById(id);
            if (_player.hasService())
            {
              //  result += this.game
                return this._serviceGames.ToString();
            }
            return this._restGames.ToString();
        }
    }
}
