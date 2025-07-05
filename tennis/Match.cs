using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tennis
{
    public class Match
    {        
        private int _currentSetIndex;        
        private int[] _playerIds;
        private List<Set> _setsToPlay;
        private EventListener _scoreboard;

        public Match(EventListener scoreboard)
        {
            this._currentSetIndex = 0;
            this._playerIds = new int[0];
            this._setsToPlay = new List<Set>();
            this._scoreboard = scoreboard;
        }       

        public void play()
        {         
            Debug.Assert(this._isValidConfig(), "Match not set yet");

            //  ScoreBoard.instance().set(this);
            // this._scoreboard.update(EventType.START_MATCH);
            PlayersManager.instance().setInitialRandomService(this._playerIds);            
            do
            {
                Set _set = new Set(this._scoreboard);              
                this._setsToPlay.Add(_set);
                //  ScoreBoard.instance().show();
                this._scoreboard.update(EventType.START_SET);
                _set.play(this._playerIds);
                this._scoreboard.update(EventType.END_SET);
                this._currentSetIndex++;                    
                PlayersManager.instance().switchService(this._playerIds);               
            } while (this._currentSetIndex < this._setsToPlay.Capacity);
            this._scoreboard.update(EventType.END_MATCH);
        }

        private bool _isValidConfig()
        {
            return PlayersManager.instance().hasPlayers() && (this._setsToPlay.Count == 3 || this._setsToPlay.Count == 5) && this._playerIds.Length == 2;
        }

        public void readConfig()
        {
            this._setsToPlay = new List<Set>(this._readSetsNum());
            this._playerIds = this._readIds();
            Console.WriteLine("Configured Match !!\n");
        }

        private int _readSetsNum()
        {
            Console.WriteLine("createMatch: ");            
            int _numSets = 0;            
            while (_numSets != this._sets(3) && _numSets != this._sets(5))
            {
                try
                {
                    Console.Write("sets: ");
                    string _sets = Console.ReadLine();
                    _numSets = int.Parse(_sets);
                    if (_numSets != this._sets(3) || _numSets != this._sets(5))
                    {
                        Console.WriteLine("invalid sets number. Should be 3 or 5.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("invalid sets number. Should be 3 or 5.");
                }
            }
            return _numSets;
        }

        private int _sets(int numSets)
        {
            return numSets;
        }

        private int[] _readIds()
        {
            Console.Write("ids: ");
            string _ids = Console.ReadLine();
            string[] _idsChunks = _ids.Split(',');
            List<int> idsArray = new List<int>();
            if (_idsChunks.Length == this._idsCount(2))
            {
                foreach (string ids in _idsChunks)
                {
                    idsArray.Add(int.Parse(ids));
                }
                if (idsArray.Count != this._idsCount(2))
                {
                    Console.WriteLine("Invalid ids number. Should be 2");
                }
            }
            else
            {
                Console.WriteLine("Invalid ids number. Should be 2");
            }
            return idsArray.ToArray();
        }     
        
        private int _idsCount(int count)
        {
            return count;
        }

        internal string toString(bool hasLack = false)
        {
            string result = "";
            foreach (int idPlayer in _playerIds)
            {
                result += PlayersManager.instance().getPlayerById(idPlayer).toString(hasLack);
                Set _set = this._setsToPlay[this._currentSetIndex];
                result += _set.toStringGamePoints(idPlayer);
                for (int i = 0; i < this._setsToPlay.Capacity; i++)
                {
                    result += this._toStringSet(i, idPlayer);
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
            }
            else
            {
                result += " -";
            }                
            return result;
        }
    }
}
