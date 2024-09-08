namespace PTerminal
{
    public class ClearCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            await Methods.ClearTerminalCommand.ClearTerminal(stackLayout, typingInterval);
        }
    }

}