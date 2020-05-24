using Microsoft.EntityFrameworkCore;

namespace Heavy.Data.Repositorys
{
    public interface IDbContextFactory
    {
        DbContext ConnWriteOrRead(WriteAndReadEnum writeAndRead);
        void ToWriteOrRead(string conn);
    }
}
