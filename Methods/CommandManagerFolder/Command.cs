public abstract class Command
{
    //abstract classes serve as a basis for other classes
    public abstract Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval);
}
