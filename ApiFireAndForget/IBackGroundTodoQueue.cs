namespace ApiFireAndForget;

public interface IBackgroundTodoQueue
{
    ValueTask PushAsync(Todo todo);
    ValueTask<Todo> PullAsync(CancellationToken cancellationToken);
}