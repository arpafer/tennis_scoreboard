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
    public class Match
    {        
        private int _currentSetIndex;        
        private int[] _playerIds;
        private List<Set> _setsToPlay;
        private IScoreBoard _scoreboard;
        private const int MAX_PLAYERS_PER_MATCH = 2;

        public Match(IScoreBoard scoreboard, List<Set> _setsToPlay, int[] _playersIds)
        {
            this._currentSetIndex = 0;
            this._playerIds = new int[0];
            this._setsToPlay = new List<Set>();
            this._scoreboard = scoreboard;
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
            if (_idsChunks.Length == MAX_PLAYERS_PER_MATCH)
            {
                foreach (string _id in _idsChunks)
                {
                    if (PlayersManager.instance().contains(int.Parse(_id))) 
                    {
                        idsArray.Add(int.Parse(_id));
                    } else
                    {
                        Console.WriteLine("Invalid id " + _ids);
                    }
                }
                if (idsArray.Count != MAX_PLAYERS_PER_MATCH)
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
