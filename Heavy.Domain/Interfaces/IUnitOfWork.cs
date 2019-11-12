using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        void BeginTransation();

        void RollBack();
        bool Commit();
    }
}
