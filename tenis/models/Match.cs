using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tennis
{
    internal class Match
    {        
        private int _currentSetIndex;        
        private int[] _playerIds;
        private List<Set> _setsToPlay;
                
        public Match()
        {
            this._currentSetIndex = 0;
            this._playerIds = new int[0];
            this._setsToPlay = new List<Set>();
        }

        public Match(List<Set> _setsToPlay, int[] _playersIds)
        {
            this._currentSetIndex = 0;
            this._playerIds = _playersIds;
            this._setsToPlay = _setsToPlay;
            PlayersManager.instance().setInitialRandomService(this._playerIds);
        }       

        public void setPoint(EventType eventType)
        {         
            Debug.Assert(this._isValidConfig(), "Match not set yet");                       
            Set _set = this._setsToPlay[this._currentSetIndex];
            if (_set != null && !_set.hasEnded())
            {
                _set.setPoint(this._playerIds, eventType);
            }
            _set = new Set();
            this._setsToPlay.Add(_set);            
            _set.setPoint(this._playerIds, eventType);
            this._currentSetIndex++;                            
        }

        private bool _isValidConfig()
        {
            return PlayersManager.instance().hasPlayers() && (this._setsToPlay.Count == 3 || this._setsToPlay.Count == 5) && this._playerIds.Length == 2;
        }             
        
        internal int[] getIdPlayers()
        {
            return this._playerIds;
        }

        internal int getNumSets()
        {
            return this._setsToPlay.Count;            
        }

        internal int getGamePoints(int playerId)
        {

        }

        internal int getSetPoints(int setIndex, int playerId)
        {
            Debug.Assert(setIndex >= 0 && setIndex < this._setsToPlay.Count, "setIndex fail");
            Set _set = this._setsToPlay[setIndex];
            return _set.getPointsOfPlayer(playerId);
        }
    }
}
