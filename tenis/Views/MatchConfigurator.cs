using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.Views
{
    internal class MatchConfigurator
    {
        private Hashtable _players;
        private Match _match;
        private const int MAX_PLAYERS_PER_MATCH = 2;

        public MatchConfigurator(Hashtable players, Match match)
        {
            this._players = players;
            this._match = match;
        }

        internal void interact()
        {
            this._registerTournamentPlayers();
            this._readMatchConfig();
        }

        private void _registerTournamentPlayers()
        {
            Console.WriteLine("Register the players: (Enter for finish)");
            bool _exit = false;
            int id = 1;
            Console.WriteLine("readPlayers");
            while (!_exit)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    _exit = true;
                }
                else
                {
                    Console.WriteLine("Name: " + name + "; id: " + id);
                    this._players.Add(id, new Player(name, id++));
                }
            }
        }

        private void _readMatchConfig()
        {            
            List<Set> _setsToPlay = new List<Set>(this._readSetsNum());
            int[] _playerIds = this._readIds();

            this._match = new Match(_setsToPlay, _playerIds);
            Console.WriteLine("Configured Match !!\n");
        }

        private int _readSetsNum()
        {
            Console.WriteLine("createMatch: ");
            int _numSets = 0;
            while (_numSets != this._setsToPlayer(3) && _numSets != this._setsToPlayer(5))
            {
                try
                {
                    Console.Write("sets: ");
                    string _sets = Console.ReadLine();
                    _numSets = int.Parse(_sets);
                    if (_numSets != this._setsToPlayer(3) || _numSets != this._setsToPlayer(5))
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

        private int _setsToPlayer(int numSets)
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
                    }
                    else
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
    }
}
