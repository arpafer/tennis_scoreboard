﻿using System;
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
        private IScoreBoard _scoreboard;
        private int _idServicePlayer;
        private int _idRestPlayer;
        private GameType _gameType;

        internal Game(IScoreBoard scoreboard, int[] idPlayers, GameType gameType)
        {
            this._gameType = gameType;
            this._isServiceLack = false;
            this._scoreboard = scoreboard;
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

        internal void play()
        {
            bool finished = false;
            while (!finished)
            {                
                int option = this._selectAction();
                switch (option)
                {
                    case (int)EventType.POINT_OF_SERVICE:                                            
                        this._pointsPair.addServicePoint();
                        this._scoreboard.update(EventType.POINT_OF_SERVICE);
                        break;
                    case (int)EventType.LACK_OF_SERVICE:
                        if (this._isServiceLack)
                        {                            
                            this._pointsPair.addRestPoint();
                            this._isServiceLack = false;
                            this._scoreboard.update(EventType.POINT_OF_REST);
                        }
                        else
                        {
                            this._isServiceLack = true;
                            this._scoreboard.update(EventType.LACK_OF_SERVICE);
                        }                        
                        break;
                    case (int)EventType.POINT_OF_REST: this._pointsPair.addRestPoint();
                        this._scoreboard.update(EventType.POINT_OF_REST);
                        break;
                }                
                finished = this._pointsPair.hasWinner();                     
            }
            this._scoreboard.update(EventType.END_GAME);
        }

        protected int _selectAction()
        {
            bool correct = false;
            while (!correct)
            {
                Console.Write("Introduce an action (1. Point of Service, 2. Lack of Service, 3. Point of rest) : ");
                string option = Console.ReadLine();
                try
                {
                    return int.Parse(option);
                }
                catch (Exception e)
                {
                    Console.WriteLine("You should enter a number from 1 to 3");
                }
            }
            return 0;
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
