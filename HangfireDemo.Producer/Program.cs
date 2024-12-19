using Hangfire;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using HangfireDemo.Shared.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Hangfire configuration

builder.Services.AddHangfire(opt =>
{
    opt.UsePostgreSqlStorage(options => options.UseNpgsqlConnection
            (builder.Configuration.GetConnectionString("PostgresConnectionString")))
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings();
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

#region Hangfire jobs

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter()
        {
            User = app.Configuration.GetSection("HangfireOptions:User").Value,
            Pass = app.Configuration.GetSection("HangfireOptions:Pass"). Value,
        }
    ]
});

#endregion

#region Recurring Jobs

RecurringJob.AddOrUpdate<ISendEmailJob>(Guid.NewGuid().ToString(),
    x => x.ExecuteAsync(), Cron.Minutely);

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();