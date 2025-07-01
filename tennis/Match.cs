using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public class Match
    {        
        private int _currentSet;        
        private int[] _playerIds;
        private Set[] _setsToPlay;

        public Match()
        {
            this._currentSet = 0;            
        }       

        public void play()
        {
            Debug.Assert((this._setsToPlay.Length == 3 || this._setsToPlay.Length == 5) && this._playerIds.Length == 2, "Match not set yet");
            this._config();
            PlayersManager.instance().setInitialRandomService(this._playerIds);            
            while (this._currentSet < this._setsToPlay.Length - 1)
            {
                Set _set = new Set();
                this._setsToPlay[this._currentSet] = _set;
                _set.play();
                if (_set.isFinished())
                {
                    this._currentSet++;                    
                    PlayersManager.instance().switchServices(this._playerIds);
                }
            }
            ScoreBoard.instance().show();
        }

        private void _config()
        {
            this._setsToPlay = new Set[this._readSetsNum()];
            this._playerIds = this._readIds();
        }

        private int _readSetsNum()
        {
            Console.WriteLine("createMatch: ");
            Console.Write("sets: ");
            string _sets = Console.ReadLine();
            int _numSets = 0;
            try
            {
                _numSets = int.Parse(_sets);
            }
            catch (Exception ex)
            {
                Console.WriteLine("invalid sets number");
            }
            return _numSets;
        }

        private int[] _readIds()
        {
            Console.Write("ids: ");
            string _ids = Console.ReadLine();
            string[] _idsChunks = Console.ReadLine().Split(',');
            List<int> idsArray = new List<int>();
            if (_idsChunks.Length == 2)
            {
                foreach (string ids in _idsChunks)
                {
                    idsArray.Add(int.Parse(ids));
                }
                if (idsArray.Count != 2)
                {
                    Console.WriteLine("Invalid ids number");
                }
            }
            else
            {
                Console.WriteLine("Invalid ids number");
            }
            return idsArray.ToArray();
        }

        internal string toString()
        {
            string result = "";
            foreach (int id in _playerIds)
            {                
                foreach (Set _set in this._setsToPlay)
                {
                    result += _set.toString(id);
                }
            }
            return result;
        }
    }
}
