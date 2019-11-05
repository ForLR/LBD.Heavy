using Heavy.Domain.Core.Commands;
using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T:Command;
        Task RaiseEvent<T>(T @event) where T:Event;
    }
}
