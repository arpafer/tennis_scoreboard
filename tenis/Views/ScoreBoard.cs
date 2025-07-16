using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tennis;

namespace tenisApp.Views
{
    internal class ScoreBoard
    {
        private Match _match;

        internal ScoreBoard(Match match)
        {
            this._match = match;
        }

        internal void interact()
        {            
            while (!this._match.isFinished())
            {                
                Console.WriteLine(this._toString());
                int _option = this._selectAction();
                this._match.setPoint((EventType)_option);
                new GameResult(this._match).interact();                
            }
        }

        internal string _toString()
        {
            string _result = "";
            Hashtable _players = this._match.getPlayers();
            foreach (int _playerKey in _players.Keys) 
            {
                _result += this._showPlayerInfo(_playerKey).PadRight(15);
                _result += this._showSetInfo(_playerKey);
                _result += "\n";
            }
            return _result;
        }

        private string _showPlayerInfo(int playerKey)
        {
            Player player = this._match.getPlayerByKey(playerKey);
            string result = "   ";
            if (player.hasLack() && player.hasService())
            {
                result = " + ";
            }
            else if (player.hasService())
            {
                result = " * ";
            }
            result += player.Name + " : ";
            return result;
        }

        private string _showSetInfo(int playerKey)
        {
            Player player = this._match.getPlayerByKey(playerKey);
            string _result = "";
            int _numSets = this._match.getNumSets();
            if (_numSets > 0)
            {
                _result += this._match.getCurrentGamePoints(player).PadRight(5);
            }
            for (int _setIndex = 0; _setIndex < _numSets; _setIndex++)
            {                
                _result += this._match.getSetPoints(_setIndex, playerKey).ToString().PadRight(5);
            }
            return _result;
        }

        protected int _selectAction()
        {
            bool correct = false;
            while (!correct)
            {
                Console.Write("Introduce an action (1. Point of Service, 2. Lack of Service, 3. Point of rest) : ");
                string option = Console.ReadLine();
                try
                {
                    return int.Parse(option);
                }
                catch (Exception e)
                {
                    Console.WriteLine("You should enter a number from 1 to 3");
                }
            }
            return 0;
        }
    }
}
