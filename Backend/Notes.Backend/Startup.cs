using Notes.Backend.Models;
using Notes.Backend.Context;
using Notes.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

public static class Startup
{
    public static void AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var Databases = typeof(Startup)
            .Assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(i => i.Name == "IDatabase"));

        var Connection = configurationManager.GetSection("Connection").Get<DataBaseConnection>()!;

        var Database = Databases.Single(x => x.Name == Connection.Provider);

        Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptions = (sp, options) => {
            var DbInstance = (IDatabase) ActivatorUtilities.CreateInstance(sp, Database, options, Connection!.ConnectionString);
            DbInstance.SetConnection();
        };

        services.AddDbContextFactory<NotesContext>(DbContextOptions, ServiceLifetime.Transient);
    }
}