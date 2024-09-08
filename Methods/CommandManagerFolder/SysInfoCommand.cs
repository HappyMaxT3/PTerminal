namespace PTerminal
{
    public class SysInfoCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            await Methods.SystemInfo.SystemInfoOut(stackLayout, typingInterval);
        }
    }
}