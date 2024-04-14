using Notes.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Notes.Backend.Databases
{    
    public abstract class Database(IServiceProvider serviceProvider, DbContextOptionsBuilder options, string Connection) : IDatabase
    {
        protected readonly IServiceProvider provider = serviceProvider;
        protected readonly string ConnectionString = Connection;
        protected readonly DbContextOptionsBuilder dbContextOptions = options;

        public abstract void SetConnection();
    }
}