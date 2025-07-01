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
        private bool _fault;

        public Player(string name, int id)
        {
            this._id = id;
            this._name = name;
            this._service = false;
            this._fault = false;
        }       

        internal void switchService()
        {
            this._service = !this._service;
        }        

        internal bool hasService()
        {
            return this._service;
        }
        
    }
}
