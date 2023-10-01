using System.Threading.Channels;

namespace ApiFireAndForget;

public class BackgroundTodoQueue : IBackgroundTodoQueue
{

    private readonly Channel<Todo> _queue;
    private readonly ILogger<BackgroundTodoQueue> _logger;
    
    public BackgroundTodoQueue(ILogger<BackgroundTodoQueue> logger)
    {
        var opts = new BoundedChannelOptions(100) { FullMode = BoundedChannelFullMode.Wait };
        _queue = Channel.CreateBounded<Todo>(opts);
        _logger = logger;
    }
    public async ValueTask PushAsync(Todo todo)
    {
        await _queue.Writer.WriteAsync(todo);
    }

    public async ValueTask<Todo> PullAsync(CancellationToken cancellationToken)
    {
        var workItem = await _queue.Reader.ReadAsync(cancellationToken);

        return workItem;
    }
}