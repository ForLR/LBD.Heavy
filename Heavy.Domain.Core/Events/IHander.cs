using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Events
{
    public interface IHander<in T> where T:Message
    {
        void Handle(T message);
    }
}
