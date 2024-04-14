using Notes.Backend.Models;
using Notes.Backend.Context;
using Notes.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Notes.Backend
{
    public static class Startup
    {
        private static Type Database { get; set; } = null!;
        private static DataBaseConnection Connection { get; set; } = null!;
        public static void AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var Databases = typeof(Startup)
                .Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(i => i.Name == "IDatabase"));

            Connection = configurationManager.GetSection("Connection").Get<DataBaseConnection>()!;

            Database = Databases.Single(x => x.Name == Connection.Provider);

            services.AddDbContextFactory<NotesContext>(DbContextOptions, ServiceLifetime.Transient);
        }

        private static void DbContextOptions(IServiceProvider sp, DbContextOptionsBuilder options)
        {
            var DbInstance = (IDatabase)ActivatorUtilities.CreateInstance(sp, Database, options, Connection.ConnectionString);
            DbInstance.SetConnection();
        }
    }
}