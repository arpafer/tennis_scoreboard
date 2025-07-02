using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    internal abstract class Game
    {
        protected bool _isServiceLack;

        protected const int POINT_OF_SERVICE = 1;
        protected const int LACK_OF_SERVICE = 2;
        protected const int POINT_OF_REST = 3;

        internal Game()
        {
            this._isServiceLack = false;
        }

        internal void play()
        {
            bool finished = false;
            while (!finished)
            {                
                int option = this._selectAction();
                switch (option)
                {
                    case POINT_OF_SERVICE:
                        this._addServicePoints();                        
                        break;
                    case LACK_OF_SERVICE:
                        if (this._isServiceLack)
                        {
                            this._addRestPoints();
                            this._isServiceLack = false;
                        }
                        else
                        {
                            this._isServiceLack = true;                            
                        }                        
                        break;
                    case POINT_OF_REST: this._addRestPoints();                        
                        break;
                }                
                finished = this._hasWinner();     
                if (!finished)
                   ScoreBoard.instance().show(this._isServiceLack);
            }
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

        internal string toString(int id)
        {
            Player _player = PlayersManager.instance().getPlayerById(id);
            string output = "";
            if (_player.hasService())
            {
                output += this._servicePointsToString();
            }
            else
            {
                output += this._restPointsToString();
            }
            return output;
        }

        protected abstract void _addServicePoints();
        protected abstract void _addRestPoints();
        protected abstract bool _hasWinner();
        protected abstract string _servicePointsToString();
        protected abstract string _restPointsToString();

    }
}
