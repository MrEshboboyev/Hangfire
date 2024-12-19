using Hangfire;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.ConfigureServices(services =>
{
    services.AddHangfire(opt =>
    {
        opt.UseSqlServerStorage(configuration.GetConnectionString("PostgresConnectionString"))
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings();
    });

    services.AddHangfireServer();
});

var host = builder.Build();
host.Run();