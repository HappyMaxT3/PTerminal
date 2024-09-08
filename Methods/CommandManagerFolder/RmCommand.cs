namespace PTerminal
{
    public class RmCommand : Command
    {
        public override async Task ExecuteAsync(StackLayout stackLayout, string argument, int typingInterval)
        {
            if (!string.IsNullOrEmpty(argument))
            {
                Android.Net.Uri fileUri = Android.Net.Uri.Parse(argument);
                _ = await Methods.DirectoryManager.DeleteFileAsync(fileUri.ToString(), stackLayout);
            }
            else
            {
                await Methods.ErrorHandler.ShowErrorAsync(stackLayout, "Provide a file to delete.", typingInterval);
            }
        }
    }

}