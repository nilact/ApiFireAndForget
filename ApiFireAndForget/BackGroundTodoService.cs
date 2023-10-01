namespace ApiFireAndForget;

public class BackGroundTodoService : BackgroundService
{
    private readonly ILogger<BackGroundTodoService> _logger;
    private readonly IBackgroundTodoQueue _taskQueue;

    public BackGroundTodoService(ILogger<BackGroundTodoService> logger, IBackgroundTodoQueue taskQueue)
    {
        _logger = logger;
        _taskQueue = taskQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string serviceName = nameof(BackGroundTodoService);
        
        _logger.LogInformation("{ServiceName} is running", serviceName);

        await ProcessTodoQueueAsync(stoppingToken);
    }
    
    private async Task ProcessTodoQueueAsync(CancellationToken stoppingToken)
    {
       
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
            try
            {
                
                await Task.Delay(5000, stoppingToken);
                
                var task = await _taskQueue.PullAsync(stoppingToken);

                //other operations - db/api call
                
                Console.WriteLine($"Todo {task.Name} retrieved and executed");
            }
            catch (OperationCanceledException operationCanceledException)
            {
                _logger.LogInformation(operationCanceledException,
                    "Operation was cancelled because host is shutting down");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing task work item");
            }
    }
}

