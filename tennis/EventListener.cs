using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tennis
{
    public interface EventListener
    {
        void update(EventType eventType);
    }
}
