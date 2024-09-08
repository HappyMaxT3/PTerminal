public abstract class Command
{
    public abstract Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval);
}
