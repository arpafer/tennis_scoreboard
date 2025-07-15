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
            while (!this._match.isEndMatch())
            {
                int _option = this._selectAction();                
                this._match.setPoint((EventType)_option);                
                Console.WriteLine(this._toString());
                new GameResult(this._match).interact();                
            }
        }

        internal string _toString()
        {
            string _result = "";
            int[] _playersId = this._match.getIdPlayers();
            foreach (int playerId in _playersId) 
            {
                _result += this._showPlayerInfo(PlayersManager.instance().getPlayerById(playerId));
                _result += this._match.getGamePoints(playerId);
                int _numSets = this._match.getNumSets();
                for (int _setIndex = 0; _setIndex < _numSets; _setIndex++)
                {
                    _result += this._match.getSetPoints(_setIndex, playerId);
                }
            }
            return _result;
        }

        private string _showPlayerInfo(Player player)
        {
            string result = "   ";
            if (player.hasLack() && player.hasService())
            {
                result = " + ";
            }
            else if (player.hasService())
            {
                result = " * ";
            }
            result += player.Id + ". " + player.Name + " : ";
            return result;
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
