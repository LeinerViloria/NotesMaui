using Notes.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Notes.Backend.Databases;

public class MySql(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptions, string Connection) : Database(serviceProvider, dbContextOptions, Connection)
{
    public override void SetConnection()
    {
        dbContextOptions.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
    }
}