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
        protected PointsManager _pointsManager;                
        protected Hashtable _players;

        internal Game(Hashtable players)
        {            
            this._players = players;
            this.initPointsType();
        }

        internal void initPointsType()
        {            
           this._pointsManager = new GamePointsManager();         
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
                    this._pointsManager.addServicePoint();
                    _playerWithService.deactivateLack();
                    break;
                case EventType.LACK_OF_SERVICE:
                    if (_playerWithService.hasLack())
                    {
                        this._pointsManager.addRestPoint();
                        _playerWithService.deactivateLack();
                    }
                    else
                    {
                        _playerWithService.activateLack();
                    }
                    break;
                case EventType.POINT_OF_REST:
                    this._pointsManager.addRestPoint();
                    _playerWithService.deactivateLack();
                    break;
            }                                
        }                                
       
        internal bool isWinnerService()
        {
            return this._pointsManager.isWinnerService();
        }

        internal string getPoints(Player player)
        {           
            if (player.hasService())
            {
                return this._pointsManager.toStringServicePoints();
            } else
            {
                return this._pointsManager.toStringRestPoints();
            }
        }

        internal bool isFinished()
        {
            return this._pointsManager.hasWinner();
        }
    }
}
