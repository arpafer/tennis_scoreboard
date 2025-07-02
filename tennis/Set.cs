using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class Set
    {        
        private List<GameNormal> _gamesNormal;
        private bool _hasTiebreak;
        private int _winnedServiceGames;
        private int _winnedRestGames;
        private bool _hasWinner;
        private Tiebreak _tiebreak;

        internal Set()
        {
            this._hasTiebreak = false;
            this._gamesNormal = new List<GameNormal>();
            this._winnedServiceGames = 0;
            this._winnedRestGames = 0;
            this._hasWinner = false;            
        }

        internal void play(int[] playersIds)
        {            
            do
            {                
                if (this._hasTiebreak)
                {
                    this._tiebreak = new Tiebreak();
                    _tiebreak.play();
                    _hasWinner = this._hasTiebreakWinner(_tiebreak);                    
                } else
                {
                    GameNormal _game = new GameNormal();
                    this._gamesNormal.Add(_game);
                    _game.play();                    
                    _hasWinner = this._hasSetWinner(_game);
                    _game.init();                    
                }
                PlayersManager.instance().switchService(playersIds);
                ScoreBoard.instance().show();                
            } while (!_hasWinner);            
        }

        private bool _hasSetWinner(GameNormal game)
        {                                  
            if (game.isWinnerService())
                this._winnedServiceGames++;
            else
                this._winnedRestGames++;

            if (this._winnedServiceGames == 6 && this._winnedRestGames == this._winnedServiceGames)
            {
                this._hasTiebreak = true;                
                return false;
            }
            if (Math.Abs(this._winnedServiceGames - this._winnedRestGames) >= 2)
            {
                return true;
            }
            return false;
        }              

        private bool _hasTiebreakWinner(Tiebreak _tiebreak)
        {
            if (_tiebreak.isWinnerService())
                this._winnedServiceGames++;
            else
                this._winnedRestGames++;

            if (Math.Abs(this._winnedServiceGames - this._winnedRestGames) >= 2)
            {
                return true;
            }
            return false;
        }

        internal bool isFinished()
        {
            return this._hasWinner;
        }
     
        internal string toString(int idPlayer)
        {
            string result = "";
            Player _player = PlayersManager.instance().getPlayerById(idPlayer);           
            if (_player.hasService())
            {
              //  result += this.game
                result += (this._isStarting())? "-": this._winnedServiceGames.ToString();
            } else
            {
                result += (this._isStarting()) ? "-": this._winnedRestGames.ToString();
            }
            return result;            
        }

        private bool _isStarting()
        {
            return this._winnedServiceGames == 0 && this._winnedRestGames == 0;
        }

        internal string toStringGamePoints(int idPlayer)
        {
            string result = "";
            Player _player = PlayersManager.instance().getPlayerById(idPlayer);
            if (this._hasTiebreak)
            {
                result = this._tiebreak.toString(idPlayer) + "  ";
            }
            else
            {
                if (this._gamesNormal.Count > 0)
                    result = this._gamesNormal[this._gamesNormal.Count - 1].toString(idPlayer) + "  ";
                else
                    result += "0  ";
            }
            return result;
        }
    }
}
