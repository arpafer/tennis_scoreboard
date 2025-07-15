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

        public void play()
        {         
            Debug.Assert(this._isValidConfig(), "Match not set yet");                       
            Set _set = this._setsToPlay[this._currentSetIndex];
            if (_set != null && !_set.hasEnded())
            {
                _set.playPoint(this._playerIds);
            }
            _set = new Set(this._scoreboard);
            this._setsToPlay.Add(_set);            
            _set.play(this._playerIds);
            this._currentSetIndex++;                            
        }

        private bool _isValidConfig()
        {
            return PlayersManager.instance().hasPlayers() && (this._setsToPlay.Count == 3 || this._setsToPlay.Count == 5) && this._playerIds.Length == 2;
        }                                

        internal string toString(bool hasLack = false)
        {
            string result = "";
            foreach (int idPlayer in _playerIds)
            {
                result += PlayersManager.instance().getPlayerById(idPlayer).toString(hasLack).PadRight(25);
                Set _set = this._setsToPlay[this._currentSetIndex];
                result += _set.toStringGamePoints(idPlayer).PadRight(5);
                for (int i = 0; i < this._setsToPlay.Capacity; i++)
                {
                    result += this._toStringSet(i, idPlayer).PadRight(5);
                }
                result += "\n";
            }            
            return result;
        }

        private string _toStringSet(int setIndex, int idPlayer)
        {           
            string result = "";            
            if (this._setsToPlay.Count > 0 && setIndex < this._setsToPlay.Count)
            {
                Set _set = this._setsToPlay[setIndex];                
                result += " " + _set.toString(idPlayer);
            } else
            {
                result += " -";
            }                     
            return result;
        }       
    }
}
