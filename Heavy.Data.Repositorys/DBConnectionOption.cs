using System.Collections.Generic;

namespace Heavy.Data.Repositorys
{
    public class DBConnectionOption
    {
        public string WriteConnection { get; set; }
        public List<string> ReadConnectionList { get; set; }
    }
}
