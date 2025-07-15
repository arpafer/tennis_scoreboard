using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenisApp.models;

namespace tennis
{
    internal class Set
    {        
        private List<Game> _gamesNormal;        
        private Dictionary<int, int> _pointsPerPlayer;              
        private Game _tiebreak;
        
        private const int DIFF_GAMES_FOR_WIN = 2;
        private const int MIN_GAMES_FOR_WIN = 6;

        internal Set()
        {            
            this._gamesNormal = new List<Game>();
            this._pointsPerPlayer = new Dictionary<int, int>();                      
        }

        internal void setPoint(Hashtable players, EventType eventType)
        {
            if (this._isTiebreak())
            {                
                this._tiebreak = new Game(players, GameType.TIEBREAK);
                this._tiebreak.setPoint(eventType);
                if (this._tiebreak.isFinished())
                {
                    this._updateSetPoints(this._tiebreak);                    
                    this._tiebreak.initPointsType();
                    this._switchService(players);
                }
            }
            else
            {
                Game _game = new Game(players, GameType.NORMAL);
                this._gamesNormal.Add(_game);
                _game.setPoint(eventType);
                if (this._isFinished(_game))
                {
                    this._updateSetPoints(_game);                    
                    _game.initPointsType();
                    this._switchService(players);
                }
            }                        
        }          

        private void _switchService(Hashtable players)
        {
            foreach (Player _player in players.Values)
            {
                _player.switchService();
            }
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

        private bool _isFinished(Game gameNormal)
        {
            int _servicePlayerId = gameNormal.getServicePlayerId();
            int _restPlayerId = gameNormal.getRestPlayerId();
            return this._somePlayerHasMinGamesForWin(_servicePlayerId, _restPlayerId) && this._somePlayerHasDiffMinGamesForWin(_servicePlayerId, _restPlayerId);            
        }                     

        private bool _somePlayerHasMinGamesForWin(int _servicePlayerId, int _restPlayerId)
        {
            return this.getPoints(_servicePlayerId) >= MIN_GAMES_FOR_WIN || this.getPoints(_restPlayerId) >= MIN_GAMES_FOR_WIN;
        }

        private bool _somePlayerHasDiffMinGamesForWin(int _servicePlayerId, int _restPlayerId)
        {
           return Math.Abs(this.getPoints(_servicePlayerId) - this.getPoints(_restPlayerId)) >= DIFF_GAMES_FOR_WIN;
        }

        private bool _isTiebreak()
        {
            if (this._gamesNormal.Count == 0)
            {
                return false;
            }
            Game gameNormal = this._gamesNormal[this._gamesNormal.Count - 1];
            int _servicePlayerId = gameNormal.getServicePlayerId();
            int _restPlayerId = gameNormal.getRestPlayerId();
            return this.getPoints(_servicePlayerId) == MIN_GAMES_FOR_WIN && this.getPoints(_servicePlayerId) == this._pointsPerPlayer[_restPlayerId];
        }        

        internal int getPoints(int playerId)
        {
            if (_pointsPerPlayer.ContainsKey(playerId))
            {
                return this._pointsPerPlayer[playerId];
            }
            return 0;
        }

        internal string getGamePoints(Player player)
        {
            Game _game = this._gamesNormal[this._gamesNormal.Count - 1];
            return _game.getPoints(player);
        }       

        internal bool isFinished()
        {
            return (this._isTiebreak() && this._tiebreak.isFinished()) ||
                       this._isFinished(this._gamesNormal[this._gamesNormal.Count - 1]);
        }

        internal bool isFinishedCurrentGame()
        {
            return this._gamesNormal[this._gamesNormal.Count - 1].isFinished();
        }

        internal bool isFinishedCurrentTiebreak()
        {
            return this._isTiebreak() && this._tiebreak.isFinished();
        }
    }
}
