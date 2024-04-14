using Notes.Backend.Models;

public static class Startup
{
    public static void AddDatabase(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        var Databases = typeof(Startup)
            .Assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(i => i.Name == "IDatabase"));

        var Connection = configurationManager.GetSection("Connection").Get<DataBaseConnection>()!;

        var Database = Databases.Single(x => x.Name == Connection.Provider);
    }
}