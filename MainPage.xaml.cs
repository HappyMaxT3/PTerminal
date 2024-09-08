using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using PTerminal.Methods;

//  dotnet build -f:net8.0-android -c:Release 
// |` terminal string to build .apk
// bin/Release/.../smth.apk

namespace PTerminal
{
    public partial class MainPage : ContentPage
    {
        private StackLayout? stackLayout; 
        public string currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private readonly string name = DeviceInfo.Name;
        public const int Typing_Interval = 1;

        [Obsolete]
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _ = Initialize();
        }

        [Obsolete]
        private async Task Initialize()
        {
            stackLayout = this.FindByName<StackLayout>("stackLayoutTerminal"); 

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await DirectoryManager.RequestFilePermissionsAsync();
            }

            if (stackLayout != null)
            {
                await WorkingProcess();
            }
            else
            {
                await ErrorHandler.ShowErrorAsync(stackLayout, "StackLayout is not initialized.", Typing_Interval);
            }
        }

        [Obsolete]
        public async Task WorkingProcess()
        {
            if(stackLayout != null)
            {
                await Greetings.TypeGreetingsAsync(stackLayout, Typing_Interval); 
                await InputCommand.TakeCommandAsync(stackLayout, name, Commands); 
            }
        }

        [Obsolete]
        private async Task Commands(string command)
        {
            command = command.Trim();
            var parts = command.Split(' '); 
            var mainCommand = parts[0];
            var argument = parts.Length > 1 ? parts[1] : null;

            switch (mainCommand)
            {
                case "man":
                    await Manual.Manuals(stackLayout, Typing_Interval); 
                    break;
                case "lshw":
                    await SystemInfo.SystemInfoOut(stackLayout, Typing_Interval);
                    break;
                case "clear":
                    await ClearTerminalCommand.ClearTerminal(stackLayout, Typing_Interval); 
                    break;
                case "ls":
                    await DirectoryManager.ListDirectoryContentsAsync(stackLayout, Typing_Interval);
                    break;
                case "cd": 
                    await DirectoryManager.SelectDirectoryAsync(stackLayout); 
                    break;
                case "rm":
                    if (!string.IsNullOrEmpty(argument))
                    {
                        Android.Net.Uri fileUri = Android.Net.Uri.Parse(argument);
                        _ = await DirectoryManager.DeleteFileAsync(fileUri.ToString(), stackLayout);
                    }
                    else
                    {
                        await ErrorHandler.ShowErrorAsync(stackLayout, "Please provide a file URI to delete.", Typing_Interval);
                    }
                    break;
                // case "pck":
                //     await DirectoryManager.PickFileAsync(stackLayout, Typing_Interval);
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
    }
}
