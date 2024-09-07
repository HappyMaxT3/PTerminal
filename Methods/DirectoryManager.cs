using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel; 
using Microsoft.Maui.Storage;

namespace PTerminal.Methods
{
    public static class DirectoryManager
    {
        // public static async Task RequestFilePermissionsAsync()
        // {
        //     var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        //     if (statusRead != PermissionStatus.Granted)
        //     {
        //         statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
        //     }

        //     var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        //     if (statusWrite != PermissionStatus.Granted)
        //     {
        //         statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
        //     }

        //     if (statusRead != PermissionStatus.Granted || statusWrite != PermissionStatus.Granted)
        //     {
        //         await Application.Current.MainPage.DisplayAlert("Permission Denied", "Storage access is required for this app to function properly.", "OK");
        //     }
        // }

            public static async Task PickFileAsync(StackLayout stackLayout, int typingInterval)
            {
                var fileResult = await FilePicker.PickAsync();
                if (fileResult != null)
                {
                    var filePath = fileResult.FullPath;

                    // Создаем метку для вывода пути файла
                    var label = new Label
                    {
                        Text = $"File picked: {filePath}",
                        TextColor = Colors.White,
                        FontFamily = "TerminalFont",
                        FontSize = 14,
                        LineHeight = 1,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start
                    };

                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
                    await Task.Delay(typingInterval);
                }
                else
                {
                    var label = new Label
                    {
                        Text = "No file was picked.",
                        TextColor = Colors.Red,
                        FontFamily = "TerminalFont",
                        FontSize = 14,
                        LineHeight = 1,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start
                    };

                    stackLayout.Children.Add(label); 
                    stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
                    await Task.Delay(typingInterval);
                }
            }


        // public static async Task<bool> DeleteFileAsync(string filePath)
        // {
        //     try
        //     {
        //         if (File.Exists(filePath))
        //         {
        //             File.Delete(filePath);
        //             return true;
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         //smth
        //     }

        //     return false;
        // }

        public static async Task ListDirectoryContents(StackLayout stackLayout, string directory, int typingInterval)
        {
            var directories = Directory.GetDirectories(directory);
            var files = Directory.GetFiles(directory);

            foreach (var dir in directories)
            {
                var label = new Label
                {
                    Text = "📁 " + Path.GetFileName(dir),
                    TextColor = Colors.White,
                    FontFamily = "TerminalFont",
                    FontSize = 14
                };
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
            }

            foreach (var file in files)
            {
                var label = new Label
                {
                    Text = "📄 " + Path.GetFileName(file),
                    TextColor = Colors.White,
                    FontFamily = "TerminalFont",
                    FontSize = 14
                };
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
            }

            await Task.Delay(typingInterval);
        }

        // public static bool ChangeDirectory(ref string currentDirectory, string newDirectory)
        // {
        //     if (Directory.Exists(newDirectory))
        //     {
        //         currentDirectory = newDirectory;
        //         return true;
        //     }

        //     return false;
        // }

        // public static async Task ShowDirectoryError(StackLayout stackLayout, string currentDirectory)
        // {
        //     var label = new Label
        //     {
        //         Text = "Error: Directory or file not found.",
        //         TextColor = Colors.Red,
        //         FontFamily = "TerminalFont",
        //         FontSize = 14,
        //         LineHeight = 1,
        //         HorizontalOptions = LayoutOptions.Start,
        //         VerticalOptions = LayoutOptions.Start
        //     };
        //     stackLayout.Children.Add(label);
        // }
    }
}
