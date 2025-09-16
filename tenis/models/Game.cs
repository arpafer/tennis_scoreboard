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
        protected Hashtable _players;

        internal Game(Hashtable players)
        {            
            this._players = players;
            this.initPointsType();
        }

        internal void initPointsType()
        {            
           this._pointsPair = new NormalPointsPair();         
        }       

        internal int getServicePlayerId()
        {
            foreach (Player _player in this._players.Values)
            {
                if (_player.hasService())
                {
                    return _player.Id;
                }
            }
            return 0;
        }

        internal int getRestPlayerId()
        {
            foreach (Player _player in this._players.Values)
            {
                if (!_player.hasService())
                {
                    return _player.Id;
                }
            }
            return 0;
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
