namespace HangfireDemo.Shared.Jobs;

public interface ISendEmailJob
{
    Task ExecuteAsync();
}