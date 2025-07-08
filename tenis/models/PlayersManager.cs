using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace tennis
{
    public class PlayersManager
    {
        private Hashtable _players;
        private static PlayersManager _instance;

        private PlayersManager()
        {
            this._players = new Hashtable();
        }

        public static PlayersManager instance()
        {
            if (_instance == null)
            {
                _instance = new PlayersManager();
            }
            return _instance;
        }

        public void registerTournamentPlayers()
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
                } else
                {
                    Console.WriteLine("Name: " + name + "; id: " + id);
                    this._players.Add(id, new Player(name, id++));
                }
            }            
        }

        public void setInitialRandomService(int []ids)
        {
            Debug.Assert(this._players.Count >= 2 && ids.Length == 2, "Players have not been configured");                       
            (this._players[ids[new Random().Next(2)]] as Player).switchService();
        }

        public void switchService(int []ids)
        {
            Debug.Assert(ids.Length == 2, "The number of ids must be two");
            foreach (int id in ids)
            {
                (this._players[id] as Player).switchService();
            }
        }

        internal Player getPlayerById(int id)
        {
            return (this._players[id] as Player);
        }

        internal bool hasPlayers()
        {
            return this._players.Count > 1;
        }

        internal bool contains(int idPlayerToSearch)
        {            
            foreach (Player _player in this._players.Values)
            {               
               if (_player.hasIdEqualTo(idPlayerToSearch))
                {
                    return true;
                }            
            }
            return false;
        }
    }
}
