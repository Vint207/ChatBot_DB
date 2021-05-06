using Microsoft.Data.SqlClient;

namespace ChatBot_DB
{
    public class BinDB : RacksDB
    {

        public void EmptyBin()
        {
            SqlCommand query = new($"DELETE [{TableId}]");
            QueryDB.ExecuteNonQuery(query);
        }
    }
}
