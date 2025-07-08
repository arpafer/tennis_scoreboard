using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal class Set
    {        
        private List<GameNormal> _gamesNormal;        
        private Dictionary<int, int> _pointsPerPlayer;      
        private bool _hasWinner;
        private Tiebreak _tiebreak;
        private IScoreBoard _scoreboard;

        private const int DIFF_GAMES_FOR_WIN = 2;
        private const int MIN_GAMES_FOR_WIN = 6;

        internal Set(IScoreBoard scoreboard)
        {            
            this._gamesNormal = new List<GameNormal>();
            this._pointsPerPlayer = new Dictionary<int, int>();
            this._hasWinner = false;
            this._scoreboard = scoreboard;
        }

        internal void play(int[] playersIds)
        {            
            do
            {                
                if (this._isTiebreak())
                {
                    this._scoreboard.update(EventType.TIEBREAK);
                    this._tiebreak = new Tiebreak(this._scoreboard, playersIds);
                    this._tiebreak.play();
                    this._updateSetPoints(this._tiebreak);                    
                    _hasWinner = true;                    
                    this._tiebreak.init();
                } else
                {
                    GameNormal _game = new GameNormal(this._scoreboard, playersIds);
                    this._gamesNormal.Add(_game);
                    _game.play();
                    this._updateSetPoints(_game);
                    _hasWinner = this._hasSetWinner(_game);
                    _game.init();                    
                }
                PlayersManager.instance().switchService(playersIds);
                if (!_hasWinner) this._scoreboard.update(EventType.UPDATE_SET);
            } while (!_hasWinner);                        
        }



        private void _updateSetPoints(Game game)
        {
            int _servicePlayerId = game.getServicePlayerId();
            int _restPlayerId = game.getRestPlayerId();
            if (game.isWinnerService())
            {
                this._updatePointPerPlayer(_servicePlayerId);
            }
            else
                this._updatePointPerPlayer(_restPlayerId);
        }

        private void _updatePointPerPlayer(int _playerId)
        {
            if (!this._pointsPerPlayer.ContainsKey(_playerId))
            {
                this._pointsPerPlayer.Add(_playerId, 1);
            } else
            {
                this._pointsPerPlayer[_playerId] = this._pointsPerPlayer[_playerId] + 1;
            }
        }

        private bool _hasSetWinner(GameNormal gameNormal)
        {
            int _servicePlayerId = gameNormal.getServicePlayerId();
            int _restPlayerId = gameNormal.getRestPlayerId();
            return this._somePlayerHasMinGamesForWin(_servicePlayerId, _restPlayerId) && this._somePlayerHasDiffMinGamesForWin(_servicePlayerId, _restPlayerId);            
        }                     

        private bool _somePlayerHasMinGamesForWin(int _servicePlayerId, int _restPlayerId)
        {
            return this._getPointsOfPlayer(_servicePlayerId) >= MIN_GAMES_FOR_WIN || this._getPointsOfPlayer(_restPlayerId) >= MIN_GAMES_FOR_WIN;
        }

        private bool _somePlayerHasDiffMinGamesForWin(int _servicePlayerId, int _restPlayerId)
        {
           return Math.Abs(this._getPointsOfPlayer(_servicePlayerId) - this._getPointsOfPlayer(_restPlayerId)) >= DIFF_GAMES_FOR_WIN;
        }

        private bool _isTiebreak()
        {
            if (this._gamesNormal.Count == 0)
            {
                return false;
            }
            GameNormal gameNormal = this._gamesNormal[this._gamesNormal.Count - 1];
            int _servicePlayerId = gameNormal.getServicePlayerId();
            int _restPlayerId = gameNormal.getRestPlayerId();
            return this._getPointsOfPlayer(_servicePlayerId) == MIN_GAMES_FOR_WIN && this._getPointsOfPlayer(_servicePlayerId) == this._pointsPerPlayer[_restPlayerId];
        }        

        private int _getPointsOfPlayer(int playerId)
        {
            if (_pointsPerPlayer.ContainsKey(playerId))
            {
                return this._pointsPerPlayer[playerId];
            }
            return 0;
        }

        private bool _hasTiebreakWinner(Tiebreak _tiebreak)
        {
            int _servicePlayerId = _tiebreak.getServicePlayerId();
            int _restPlayerId = _tiebreak.getRestPlayerId();
            return this._somePlayerHasDiffMinGamesForWin(_servicePlayerId, _restPlayerId);            
        }       
     
        internal string toString(int idPlayer)
        {
            string result = "";
            Player _playerToString = PlayersManager.instance().getPlayerById(idPlayer);
            if (this._gamesNormal.Count == 0)
            {
                return "-";
            }            
            Game lastGame = this._gamesNormal[this._gamesNormal.Count - 1];
            Player _playerWithServiceInLastGame = lastGame.getServicePlayer();
         //   if (_playerToString.hasIdEqualTo(_playerWithServiceInLastGame))
           // {
                result += this._getPointsOfPlayer(idPlayer).ToString();
         //   }
         //   else
         //   {
           //     result += (this._isStarting()) ? "-" : this._getPointsOfPlayer(idPlayer).ToString();
           // }            
            return result;            
        }

        private bool _isStarting()
        {
            bool _starting = true;
            foreach (int points in _pointsPerPlayer.Values)
            {
                if (points != 0)
                {
                    _starting = false;
                }
            }
            return _starting;
        }

        internal string toStringGamePoints(int idPlayer)
        {
            string result = "";
            Player _player = PlayersManager.instance().getPlayerById(idPlayer);
            if (this._tiebreak != null)
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
