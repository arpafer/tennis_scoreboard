using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public enum EventType
    {        
        POINT_OF_SERVICE = 1,
        LACK_OF_SERVICE = 2,
        POINT_OF_REST = 3,
        START_MATCH = 4,
        START_SET = 5,        
        END_SET = 6,
        END_MATCH = 7,
        END_GAME = 8,
        UPDATE_SET = 9
    }
}
