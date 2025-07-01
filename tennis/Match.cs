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
        private Set[] _sets;

        public Match(int numSets, int[] ids)
        {
            this._currentSet = 0;
            this._sets = new Set[numSets];
            this._playerIds = ids;
        }        
        
        public void play()
        {
            Debug.Assert((this._sets.Length == 3 || this._sets.Length == 5) && this._playerIds.Length == 2, "Match not set yet");
            PlayersManager.instance().

            while (this._currentSet < this._sets.Length - 1)
            {
                Set _set = new Set();
                this._sets[this._currentSet] = _set;
                _set.play();
                if (_set.isFinish())
                {
                    this._currentSet++;
                    this._show();
                }
            }
        }

        private void _show()
        {

        }
    }
}
