using Microsoft.EntityFrameworkCore;

namespace Heavy.Data.Repositorys
{
    public interface IWriteOrRead
    {
        
        DbContext GetDbContext(string conn);


    }
}
