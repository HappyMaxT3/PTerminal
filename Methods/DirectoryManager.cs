using Android.Content;
using AndroidX.DocumentFile.Provider;
using Android.App;

namespace PTerminal.Methods
{
    public static class DirectoryManager
    {
        private static Android.Net.Uri? _currentDirectoryUri;
        private const int RequestCodeSelectDirectory = 42; //request code

        public static async Task RequestFilePermissionsAsync()
        {
            //request file permissions
            var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (statusRead != PermissionStatus.Granted)
            {
                statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (statusWrite != PermissionStatus.Granted)
            {
                statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (statusRead != PermissionStatus.Granted || statusWrite != PermissionStatus.Granted)
            {
                // await DisplayAlert("Permission Denied", "Storage access is required.", "OK");
            }
        }

        public static async Task SelectDirectoryAsync(StackLayout stackLayout)
        {
            try
            {
                //open explorer window
                var intent = new Intent(Intent.ActionOpenDocumentTree);
                intent.AddFlags(ActivityFlags.GrantPersistableUriPermission |
                                ActivityFlags.GrantReadUriPermission |
                                ActivityFlags.GrantWriteUriPermission);

                Platform.CurrentActivity.StartActivityForResult(intent, RequestCodeSelectDirectory);
            }
            catch (Exception ex)
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, $"App-error: {ex.Message}", MainPage.Typing_Interval);
            }
        }

        public static void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //method for successful directory choosing
            //saving permissions
            if (requestCode == RequestCodeSelectDirectory && resultCode == Result.Ok && data != null)
            {
                _currentDirectoryUri = data.Data;
                if (_currentDirectoryUri != null)
                {
                    Platform.CurrentActivity.ContentResolver.TakePersistableUriPermission(_currentDirectoryUri,
                        data.Flags & (ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission)
                    );
                }
            }
        }

        private static string GetDirectoryName(Android.Net.Uri directoryUri)
        {
            var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, directoryUri);
            return directory?.Name ?? "Unknown Directory";
        }

        public static async Task ListDirectoryContentsAsync(StackLayout stackLayout, int typingInterval)
        {
            if (_currentDirectoryUri == null)
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, "No directory selected >:(", typingInterval);
                return;
            }

            try
            {
                var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, _currentDirectoryUri);
                if (directory == null || !directory.IsDirectory)
                {
                    await ErrorHandler.ShowErrorAsync(stackLayout, "Invalid directory >:(", typingInterval);
                    return;
                }

                stackLayout.Children.Add(new Label
                {
                    Text = $"📁 {GetDirectoryName(_currentDirectoryUri)}",
                    TextColor = Colors.LightYellow,
                    FontSize = 15,
                    FontFamily = "TerminalFont",
                    LineHeight = 1
                });

                var files = directory.ListFiles();
                if (files == null || files.Length == 0)
                {
                    await ErrorHandler.ShowErrorAsync(stackLayout, "No files found...", typingInterval);
                    return;
                }

                foreach (var file in files)
                {
                    stackLayout.Children.Add(new Label
                    {
                        Text = file.IsDirectory ? $"|- 📁 {file.Name}" : $"|- 📄 {file.Name}",
                        TextColor = Colors.AntiqueWhite,
                        FontSize = 15,
                        FontFamily = "TerminalFont",
                        LineHeight = 1
                    });
                    await Task.Delay(typingInterval);
                }
            }
            catch (Exception ex)
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, $"App-error: {ex.Message}", typingInterval);
            }
            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }

        public static async Task<bool> DeleteFileAsync(string fileName, StackLayout stackLayout)
        {
            if (_currentDirectoryUri == null)
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, "No directory selected.", MainPage.Typing_Interval);
                return false;
            }

            try
            {
                var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, _currentDirectoryUri);
                if (directory == null || !directory.IsDirectory)
                {
                    await ErrorHandler.ShowErrorAsync(stackLayout, "Invalid directory >:(", MainPage.Typing_Interval);
                    return false;
                }

                var fileToDelete = directory.ListFiles()?.FirstOrDefault(file => file.Name == fileName && file.IsFile);
                if (fileToDelete == null)
                {
                    await ErrorHandler.ShowErrorAsync(stackLayout, $"File {fileName} not found :(", MainPage.Typing_Interval);
                    return false;
                }

                bool success = fileToDelete.Delete();
                if (success)
                {
                    stackLayout.Children.Add(new Label
                    {
                        Text = $"File {fileName} deleted successfully.",
                        TextColor = Colors.GreenYellow,
                        FontSize = 15,
                        FontFamily = "TerminalFont",
                        LineHeight = 1
                    });
                    return true;
                }

                await ErrorHandler.ShowErrorAsync(stackLayout, $"Unable to delete file {fileName}.", MainPage.Typing_Interval);
                return false;
            }
            catch (Exception ex)
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, $"App-error: {ex.Message}", MainPage.Typing_Interval);
                return false;
            }
        }
    }
}
