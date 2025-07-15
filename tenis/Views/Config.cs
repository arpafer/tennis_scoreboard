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
    internal class Config
    {
        private Hashtable _players;
        private Match _match;

        public Config(Hashtable players, Match match)
        {
            this._players = players;
            this._match = match;
        }

        void interact()
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
    }
}
