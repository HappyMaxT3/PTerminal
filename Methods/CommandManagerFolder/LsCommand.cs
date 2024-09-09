namespace PTerminal
{
    public class LsCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            await Methods.DirectoryManager.ListDirectoryContentsAsync(stackLayout, typingInterval);
        }
    }
}