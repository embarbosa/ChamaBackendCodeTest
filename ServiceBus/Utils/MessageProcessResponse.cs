using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.WebApi.ServiceBus
{
    public enum MessageProcessResponse
    {
        Complete,
        Abandon,
        Dead
    }
}
