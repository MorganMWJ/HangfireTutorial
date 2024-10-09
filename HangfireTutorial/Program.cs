using Hangfire;
using HangfireTutorial;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(x =>
    x.UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage("Server=localhost;Database=Hangfire;User Id=sa;Password=Passw0rd;TrustServerCertificate=True;")
    );

builder.Services.AddTransient<ExampleJob>();

builder.Services.AddHangfireServer(x => x.SchedulePollingInterval = TimeSpan.FromSeconds(1));

var app = builder.Build();

app.UseHangfireDashboard();

app.MapGet("/job", (IBackgroundJobClient jobClient, IRecurringJobManager jobManager) =>
{
    //jobClient.Enqueue(() => Console.WriteLine("Hello from BG"));
    
    jobClient.Schedule(() => Console.WriteLine("Hello from BG 2"), TimeSpan.FromSeconds(5));
    jobManager.AddOrUpdate("everyMinuteCronJob", () => Console.WriteLine("Hello from every 1 minute CRON job"), "*/1 * * * *");
    
    jobClient.Schedule<ExampleJob>(x => x.DoSomething(), TimeSpan.FromSeconds(5));
    return Results.Ok("Hello job!");
});



app.Run();
