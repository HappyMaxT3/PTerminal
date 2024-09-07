using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Android.Content;
using Android.Provider;
using AndroidX.DocumentFile.Provider;
using Android.App;
using Android.OS;

namespace PTerminal.Methods
{
    public static class DirectoryManager
    {
        private static Android.Net.Uri currentDirectoryUri; 
        private const int RequestCodeSelectDirectory = 42; 

        public static async Task RequestFilePermissionsAsync()
        {
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
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Permission Denied", "Storage access is required for this app to function properly.", "OK");
            }
        }

        public static async Task SelectDirectoryAsync(StackLayout stackLayout)
        {
            try
            {
                var intent = new Intent(Intent.ActionOpenDocumentTree);
                intent.AddFlags(ActivityFlags.GrantPersistableUriPermission | ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission);
                var activity = Platform.CurrentActivity;
                activity.StartActivityForResult(intent, RequestCodeSelectDirectory);
            }
            catch (Exception ex)
            {
                var label = new Label
                {
                    Text = $"Error: {ex.Message}",
                    TextColor = Colors.Red,
                    FontSize = 14
                };
                stackLayout.Children.Add(label);
            }
        }

        public static void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RequestCodeSelectDirectory && resultCode == Result.Ok && data != null)
            {
                currentDirectoryUri = data.Data;
                var contentResolver = Android.App.Application.Context.ContentResolver;

                if (currentDirectoryUri != null)
                {
                    contentResolver.TakePersistableUriPermission(currentDirectoryUri, 
                        data.Flags & (ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission));
                }
            }
        }

        public static async Task ListDirectoryContentsAsync(StackLayout stackLayout, int typingInterval)
        {
            if (currentDirectoryUri == null)
            {
                await ShowErrorAsync(stackLayout, "No directory selected.");
                return;
            }

            try
            {
                var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, currentDirectoryUri);
                if (directory != null && directory.IsDirectory)
                {
                    var files = directory.ListFiles();
                    foreach (var file in files)
                    {
                        var label = new Label
                        {
                            Text = file.IsDirectory ? $"📁 {file.Name}" : $"📄 {file.Name}",
                            TextColor = Colors.White,
                            FontSize = 14
                        };
                        stackLayout.Children.Add(label);
                        await Task.Delay(typingInterval);
                    }
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(stackLayout, $"Error: {ex.Message}");
            }
        }

        public static async Task<bool> DeleteFileAsync(Android.Net.Uri fileUri, StackLayout stackLayout)
        {
            try
            {
                var file = DocumentFile.FromSingleUri(Platform.CurrentActivity, fileUri);
                if (file != null && file.Exists() && file.IsFile)
                {
                    bool success = file.Delete();
                    if (success)
                    {
                        var label = new Label
                        {
                            Text = $"File {file.Name} deleted successfully.",
                            TextColor = Colors.White,
                            FontSize = 14
                        };
                        stackLayout.Children.Add(label);
                        return true;
                    }
                }

                await ShowErrorAsync(stackLayout, "Error: Unable to delete file.");
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(stackLayout, $"Error: {ex.Message}");
            }

            return false;
        }

        private static async Task ShowErrorAsync(StackLayout stackLayout, string errorMessage)
        {
            var label = new Label
            {
                Text = errorMessage,
                TextColor = Colors.Red,
                FontSize = 14
            };
            stackLayout.Children.Add(label);
            await Task.Delay(1000);
        }
    }
}
