using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public class Player
    {
        private int _id;
        private string _name;
        private bool _service;        

        public Player(string name, int id)
        {
            this._id = id;
            this._name = name;
            this._service = false;            
        }       

        internal bool hasIdEqualTo(Player player)
        {
            return this._id == player._id;
        }

        internal void switchService()
        {
            this._service = !this._service;
        }        

        internal bool hasService()
        {
            return this._service;
        }              

        internal string toString(bool hasLack)
        {
            string result = "   ";
            if (hasLack && this.hasService())
            {
                result = " + ";
            } else if (this.hasService())
            {
                result = " * ";
            }            
            result += this._id + ". " + this._name + " : ";
            return result;
        }
    }
}
