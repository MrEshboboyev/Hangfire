using Hangfire;
using Hangfire.PostgreSql;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.ConfigureServices(services =>
{
    services.AddHangfire(opt =>
    {
        opt.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(
                configuration.GetConnectionString("HangfireDemo_DB")))
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings();
    });

    services.AddHangfireServer();
});

var host = builder.Build();
host.Run();