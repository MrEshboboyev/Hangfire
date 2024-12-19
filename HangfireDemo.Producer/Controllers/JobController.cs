﻿using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController(IBackgroundJobClient backgroundJobClient)
{
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;

    [HttpPost]
    [Route("fire-and-forget")]
    public Task<string> FireAndForget([FromBody] string text)
    {
        _backgroundJobClient.Enqueue(() => Console.WriteLine(text));
        return Task.FromResult(text);
    }
    
    [HttpPost]
    [Route("delayed")]
    public Task<string> Delayed([FromBody] string text)
    {
        _backgroundJobClient.Schedule(() => Console.WriteLine(text),
            TimeSpan.FromMinutes(1));
        return Task.FromResult(text);
    }
    
    [HttpPost]
    [Route("continuation")]
    public Task<string> Continuation([FromBody] string text)
    {
        var jobId = _backgroundJobClient.Schedule(() => Console.WriteLine(text),
            TimeSpan.FromMinutes(1));
        _backgroundJobClient.ContinueJobWith(jobId, () => Console.WriteLine(text));
        
        return Task.FromResult(text);
    }
}