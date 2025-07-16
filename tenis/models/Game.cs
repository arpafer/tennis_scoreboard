using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using tenisApp.models;

namespace tennis
{
    internal class Game
    {        
        protected PointsPair _pointsPair;        
        private GameType _gameType;
        private Hashtable _players;

        internal Game(Hashtable players, GameType gameType)
        {
            this._gameType = gameType;            
            this._players = players;            
            this.initPointsType();
        }

        internal void initPointsType()
        {
            if (this._gameType == GameType.NORMAL)
            {
                this._pointsPair = new NormalPointsPair();
            }
            else
            {
                this._pointsPair = new TiebreakPointsPair();
            }
        }       

        internal int getServicePlayerId()
        {
            if ((this._players[0] as Player).hasService())
            {
                return 0;
            } else
            {
                return 1;
            }
        }

        internal int getRestPlayerId()
        {
            if (!(this._players[0] as Player).hasService())
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        internal void setPoint(EventType eventType)
        {
            Player _playerWithService = this._players[this.getServicePlayerId()] as Player;
            switch (eventType)
            {
                case EventType.POINT_OF_SERVICE:
                    this._pointsPair.addServicePoint();
                    _playerWithService.deactivateLack();
                    break;
                case EventType.LACK_OF_SERVICE:
                    if (_playerWithService.hasLack())
                    {
                        this._pointsPair.addRestPoint();
                        _playerWithService.deactivateLack();
                    }
                    else
                    {
                        _playerWithService.activateLack();
                    }
                    break;
                case EventType.POINT_OF_REST:
                    this._pointsPair.addRestPoint();
                    _playerWithService.deactivateLack();
                    break;
            }                                
        }                                
       
        internal bool isWinnerService()
        {
            return this._pointsPair.isWinnerService();
        }

        internal string getPoints(Player player)
        {           
            if (player.hasService())
            {
                return this._pointsPair.toStringServicePoints();
            } else
            {
                return this._pointsPair.toStringRestPoints();
            }
        }

        internal bool isFinished()
        {
            return this._pointsPair.hasWinner();
        }
    }
}
