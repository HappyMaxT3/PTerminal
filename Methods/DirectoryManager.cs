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
        private static Android.Net.Uri? currentDirectoryUri; 
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
                //await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
                    // "Permission Denied", 
                    // "Storage access is required for this app to function properly.", 
                    // "OK"
                //);
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
                await ShowErrorAsync(stackLayout, $"Error: {ex.Message}");
            }
        }

        public static void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RequestCodeSelectDirectory && resultCode == Result.Ok && data != null)
            {
                currentDirectoryUri = data.Data;

                var contentResolver = Platform.CurrentActivity.ContentResolver;
                if (currentDirectoryUri != null)
                {
                    contentResolver.TakePersistableUriPermission(currentDirectoryUri, data.Flags & (ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission));
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
            if (currentDirectoryUri == null)
            {
                await ShowErrorAsync(stackLayout, "No directory selected.");
                return;
            }

            try
            {
                var directoryName = GetDirectoryName(currentDirectoryUri);
                var directoryLabel = new Label
                {
                    Text = $"📁 {directoryName}",
                    TextColor = Colors.LightYellow,
                    FontSize = 15,
                    FontFamily = "TerminalFont",
                    LineHeight = 1,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                stackLayout.Children.Add(directoryLabel);

                var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, currentDirectoryUri);
                if (directory != null && directory.IsDirectory)
                {
                    var files = directory.ListFiles();
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            var label = new Label
                            {
                                Text = file.IsDirectory ? $"|- 📁 {file.Name}" : $"|- 📄 {file.Name}",
                                TextColor = Colors.AntiqueWhite,
                                FontSize = 15,
                                FontFamily = "TerminalFont",
                                LineHeight = 1,
                                HorizontalOptions = LayoutOptions.Start,
                                VerticalOptions = LayoutOptions.Start
                            };
                            stackLayout.Children.Add(label);
                            await Task.Delay(typingInterval);
                        }
                    }
                    else
                    {
                        await ShowErrorAsync(stackLayout, "No files found in the directory.");
                    }
                }
                else
                {
                    await ShowErrorAsync(stackLayout, "Invalid directory.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(stackLayout, $"Error: {ex.Message}");
            }
            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }

        public static async Task<bool> DeleteFileAsync(string fileName, StackLayout stackLayout)
        {
            try
            {
                if (currentDirectoryUri == null)
                {
                    await ShowErrorAsync(stackLayout, "No directory selected.");
                    return false;
                }

                var directory = DocumentFile.FromTreeUri(Platform.CurrentActivity, currentDirectoryUri);
                if (directory == null || !directory.IsDirectory)
                {
                    await ShowErrorAsync(stackLayout, "Invalid directory.");
                    return false;
                }

                var files = directory.ListFiles();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Name == fileName && file.IsFile)
                        {
                            bool success = file.Delete();
                            if (success)
                            {
                                var label = new Label
                                {
                                    Text = $"File {fileName} deleted successfully.",
                                    TextColor = Colors.GreenYellow,
                                    FontSize = 15,
                                    FontFamily = "TerminalFont",
                                    LineHeight = 1,
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start
                                };
                                stackLayout.Children.Add(label);
                                return true;
                            }
                            else
                            {
                                await ShowErrorAsync(stackLayout, $"Error: Unable to delete file {fileName}.");
                                return false;
                            }
                        }
                    }
                }
                await ShowErrorAsync(stackLayout, $"Error: File {fileName} not found.");
                return false;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(stackLayout, $"App-error: {ex.Message}");
                return false;
            }
        }

        private static async Task ShowErrorAsync(StackLayout stackLayout, string errorMessage)
        {
            var label = new Label
            {
                Text = errorMessage,
                TextColor = Colors.Red,
                FontFamily = "TerminalFont",
                FontSize = 14,
                LineHeight = 1,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            stackLayout.Children.Add(label);
            await Task.Delay(1000);
        }

        internal static async Task DeleteFileAsync(Android.Net.Uri? fileUri, StackLayout? stackLayout)
        {
            throw new NotImplementedException();
        }
    }
}
