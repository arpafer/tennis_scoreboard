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

        public MatchConfigurator(Match match)
        {
            this._players = new Hashtable();
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
            while (id <= 2)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();                
                Console.WriteLine("Name: " + name + "; id: " + id);
                this._players.Add(id - 1, new Player(name, id++));                
            }                        
        }

        private void _readMatchConfig()
        {            
            List<Set> _setsToPlay = new List<Set>(this._readSetsNum());            
            this._match.set(_setsToPlay, this._players);
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
    }
}
