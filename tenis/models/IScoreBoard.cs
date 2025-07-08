using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public interface IScoreBoard
    {
        void update(EventType eventType);
    }
}
