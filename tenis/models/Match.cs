using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tennis
{
    internal class Match
    {        
        private int _currentSetIndex;        
        private Hashtable _players;
        private List<Set> _setsToPlay;
                
        public Match()
        {
            this._currentSetIndex = 0;
            this._players = new Hashtable();
            this._setsToPlay = new List<Set>();
        }

        public void set(List<Set> _setsToPlay, Hashtable _players)
        {
            this._currentSetIndex = 0;
            this._players = _players;
            this._setsToPlay = _setsToPlay;            
            (this._players[new Random().Next(2)] as Player).switchService();
        }

        internal bool isFinished()
        {
            if (this._currentSetIndex >= this._setsToPlay.Capacity) { 
               return this._setsToPlay[this._currentSetIndex - 1].isFinished();               
            }
            return false;
        }

        internal bool isFinishedCurrentSet()
        {
            return this._setsToPlay[this._currentSetIndex - 1].isFinished();
        }

        internal bool isFinishedCurrentGame()
        {
            return this._setsToPlay[this._currentSetIndex - 1].isFinishedCurrentGame();
        }

        internal bool isStartTiebreak()
        {
            return this._setsToPlay[this._currentSetIndex - 1].isStartTiebreak();
        }

        public void setPoint(EventType eventType)
        {         
            Debug.Assert(this._isValidConfig(), "Match not set yet");
            Set _set = null;
            if (this._setsToPlay.Count > 0 && this._setsToPlay[this._currentSetIndex - 1] != null)
            {
                _set = this._setsToPlay[this._currentSetIndex - 1];
                if (_set.isFinished())
                {
                    _set = this._addNewSet();
                }
            }
            if (_set == null) 
            {
                _set = this._addNewSet();
            }                             
            _set.setPoint(this._players, eventType);            
        }

        private Set _addNewSet()
        {
            Set _set = new Set();
            this._setsToPlay.Add(_set);
            this._currentSetIndex++;
            return _set;
        }

        private bool _isValidConfig()
        {
            return (this._setsToPlay.Count == 3 || this._setsToPlay.Count == 5) && this._players.Count == 2;
        }             
        
        internal Hashtable getPlayers()
        {
            return this._players;
        }

        internal int getNumSets()
        {
            return this._setsToPlay.Count;            
        }

        internal string getCurrentGamePoints(Player player)
        {
            Debug.Assert(player != null, "player fail");
            Set _set = this._setsToPlay[this._currentSetIndex - 1];
            return _set.getGamePoints(player);
        }

        internal int getSetPoints(int setIndex, int playerId)
        {
            Debug.Assert(setIndex >= 0 && setIndex < this._setsToPlay.Count, "setIndex fail");
            Set _set = this._setsToPlay[setIndex];
            return _set.getPoints(playerId);
        }

        internal Player getPlayerByKey(int playerId)
        {
            Debug.Assert(this._players.ContainsKey(playerId), "playerId should exist");
            return this._players[playerId] as Player;
        }

        internal void initCurrentGame()
        {
            Set _set = this._setsToPlay[this._setsToPlay.Count - 1];
            _set.initCurrentGame();
        }       
    }
}
