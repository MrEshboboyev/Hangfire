using Hangfire;
using Hangfire.PostgreSql;
using HangfireDemo.Producer;
using HangfireDemo.Server.Jobs;
using HangfireDemo.Server.Options;
using HangfireDemo.Shared.Jobs;
using HangfireDemo.Shared.Services;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.ConfigureServices(services =>
{
    services.AddHangfire(opt =>
    {
        opt.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(
                configuration.GetConnectionString("PostgresConnectionString")))
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings();
    });

    services.AddHangfireServer();
    services.AddScoped<ISendEmailJob, SendEmailJob>();
    services.AddScoped<IEmailService, EmailService>();
    
    // Server options configuration
    services.Configure<ServerOptions>(configuration.GetSection(ServerOptions.ServerOptionsKey));
});

var host = builder.Build();
host.Run();