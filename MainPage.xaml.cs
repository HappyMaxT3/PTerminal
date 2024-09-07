using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using PTerminal.Methods;

namespace PTerminal
{
    public partial class MainPage : ContentPage
    {
        private StackLayout stackLayout; 
        public string currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private readonly string name = DeviceInfo.Name;
        public const int TypingInterval = 1;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Initialize();

        }

        private async Task Initialize()
        {
            stackLayout = this.FindByName<StackLayout>("stackLayoutTerminal"); 

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await DirectoryManager.RequestFilePermissionsAsync();
            }
            
            await WorkingProcess();
        }

        public async Task WorkingProcess()
        {
            await Greetings.TypeGreetingsAsync(stackLayout, TypingInterval); 
            await InputCommand.TakeCommandAsync(stackLayout, name, Commands); 
        }

        private async Task Commands(string command)
        {
            command = command.Trim().ToLower();
            var parts = command.Split(' '); 
            var mainCommand = parts[0];
            var argument = parts.Length > 1 ? parts[1] : null;

            switch (mainCommand)
            {
                case "man":
                    await Manual.Manuals(stackLayout, TypingInterval); 
                    break;
                case "lshw":
                    await SystemInfo.SystemInfoOut(stackLayout, TypingInterval);
                    break;
                case "clear":
                    await ClearTerminalCommand.ClearTerminal(stackLayout, TypingInterval); 
                    break;
                case "ls":
                    await DirectoryManager.ListDirectoryContentsAsync(stackLayout, TypingInterval);
                    break;
                case "cd": 
                    await DirectoryManager.SelectDirectoryAsync(stackLayout); 
                    break;
                case "rm":
                    if (!string.IsNullOrEmpty(argument))
                    {
                        Android.Net.Uri fileUri = Android.Net.Uri.Parse(argument);
                        await DirectoryManager.DeleteFileAsync(fileUri, stackLayout);
                    }
                    else
                    {
                        await ShowError("Please provide a file URI to delete.");
                    }
                    break;

                // case "pck":
                //     await DirectoryManager.PickFileAsync(stackLayout, TypingInterval);
                //     stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
                //     break;
                default:
                    var label = new Label
                    {
                        Text = "Command not found :(",
                        TextColor = Colors.Red,
                        FontFamily = "TerminalFont",
                        FontSize = 14,
                        LineHeight = 1,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start
                    };
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
                    break;
            }

            await InputCommand.TakeCommandAsync(stackLayout, name, Commands); 
        }
        private async Task ShowError(string message)
        {
            var label = new Label
            {
                Text = message,
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
    }
}
