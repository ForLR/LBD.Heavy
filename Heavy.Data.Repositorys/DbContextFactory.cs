using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Heavy.Data.Repositorys
{
    public class DbContextFactory : IDbContextFactory
    {
        public DBConnectionOption dBConnectionOption { get; set; }

        public DbContext dbContext { get; set; }

        public DbContextFactory(IOptionsMonitor<DBConnectionOption> dBConnectionOption, DbContext dbContext)
        {
            this.dBConnectionOption = dBConnectionOption.CurrentValue;
            this.dbContext = dbContext;
        }

        public DbContext ConnWriteOrRead(WriteAndReadEnum writeAndRead)
        {
            switch (writeAndRead)
            {
                case WriteAndReadEnum.Write:
                    ToWrite();
                    break;
                case WriteAndReadEnum.Read:
                    ToRead();
                    break;
                default:
                    throw new Exception();
            }
            return dbContext;
        }

        public void ToWriteOrRead(string conn)
        {
            if (dbContext is IWriteOrRead)
            {
                var writeOrReadContext = dbContext as IWriteOrRead;
                this.dbContext = writeOrReadContext.GetDbContext(conn);
            }

        }


        private void ToWrite()
        {
            var conn = dBConnectionOption.WriteConnection;
            ToWriteOrRead(conn);// dbContext.ToWriteOrRead(conn);
        }
        private void ToRead()
        {
            var conn = dBConnectionOption.ReadConnectionList[0];
            ToWriteOrRead(conn);
        }


    }
}
