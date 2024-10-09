namespace HangfireTutorial;

public class ExampleJob
{
    private readonly ILogger<ExampleJob> _logger;

    public ExampleJob(ILogger<ExampleJob> logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        _logger.LogInformation("I did something");
    }
}
