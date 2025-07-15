using System;
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
        protected bool _isServiceLack;                      
        private int _idServicePlayer;
        private int _idRestPlayer;
        private GameType _gameType;

        internal Game(int[] idPlayers, GameType gameType)
        {
            this._gameType = gameType;
            this._isServiceLack = false;            
            if (PlayersManager.instance().getPlayerById(idPlayers[0]).hasService())
            {
                this._idServicePlayer = idPlayers[0];
                this._idRestPlayer = idPlayers[1];
            } else
            {
                this._idServicePlayer = idPlayers[1];
                this._idRestPlayer = idPlayers[0];
            }
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

        internal Player getServicePlayer()
        {
            return PlayersManager.instance().getPlayerById(this._idServicePlayer);
        }

        internal int getServicePlayerId()
        {
            return this._idServicePlayer;
        }

        internal int getRestPlayerId()
        {
            return this._idRestPlayer;
        }

        internal void setPoint(EventType eventType)
        {                                            
            switch (eventType)
            {
                case EventType.POINT_OF_SERVICE:
                    this._pointsPair.addServicePoint();                    
                    break;
                case EventType.LACK_OF_SERVICE:
                    if (this._isServiceLack)
                    {
                        this._pointsPair.addRestPoint();
                        this._isServiceLack = false;                        
                    }
                    else
                    {
                        this._isServiceLack = true;                        
                    }
                    break;
                case EventType.POINT_OF_REST:
                    this._pointsPair.addRestPoint();                    
                    break;
            }                                
        }                 

        internal string toString(int id)
        {
            Player _player = PlayersManager.instance().getPlayerById(id);
            string output = "";
            if (_player.hasService())
            {                
                output += this._servicePointsToString();
            }
            else
            {
                output += this._restPointsToString();
            }
            return output;
        }

        internal string _servicePointsToString()
        {
            return this._pointsPair.toStringServicePoints();
        }

        internal string _restPointsToString()
        {
            return this._pointsPair.toStringRestPoints();
        }
       
        internal bool isWinnerService()
        {
            return this._pointsPair.isWinnerService();
        }
    }
}
