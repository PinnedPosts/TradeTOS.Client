using SQLite.Net;

namespace TradeTOS.Abstractions
{

    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
