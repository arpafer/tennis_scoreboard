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
        private bool _lack;

        public Player(string name, int id)
        {
            this._id = id;
            this._name = name;
            this._service = false;
            this._lack = false;
        }       
        
        public string Name { get { return this._name; } }

        internal bool isEqualTo(Player player)
        {
            return this._id == player._id && this._name == player._name && this._service == player._service;
        }

        internal void activateLack()
        {
            this._lack = true;
        }

        internal void deactivateLack()
        {
            this._lack = false;
        }

        internal bool hasIdEqualTo(int id)
        {
            return this._id == id;
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
        
        internal bool hasLack()
        {
            return this._lack;
        }
    }
}
