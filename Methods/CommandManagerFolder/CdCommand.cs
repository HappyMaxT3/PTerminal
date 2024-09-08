namespace PTerminal
{
    public class CdCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            await Methods.DirectoryManager.SelectDirectoryAsync(stackLayout);
        }
    }
}