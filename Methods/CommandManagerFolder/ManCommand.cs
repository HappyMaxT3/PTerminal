using PTerminal.Methods;

namespace PTerminal
{
    public class ManCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            await Manual.Manuals(stackLayout, typingInterval);
        }
    }

}