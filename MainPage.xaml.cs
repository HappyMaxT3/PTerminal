using PTerminal.Methods;

//  dotnet build -f:net8.0-android -c:Release 
// |` terminal string to build .apk
// bin/Release/.../smth.apk

namespace PTerminal
{
    public partial class MainPage : ContentPage
    {
        private StackLayout? stackLayout; // ? - can be null

        private CommandManager _commandManager;
        public string currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private readonly string name = DeviceInfo.Name; // assignment only at initialization
        public const int Typing_Interval = 1;

        [Obsolete]
        public MainPage()
        {
            _commandManager = new CommandManager();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _ = Initialize();
        }

        [Obsolete]
        private async Task Initialize()
        {
            //asynchronous methods block the interface (but doing) 
            //when performing long operations

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
            // take commands

            command = command.Trim();
            var parts = command.Split(' '); 
            var mainCommand = parts[0];
            var argument = parts.Length > 1 ? parts[1] : null;

            await _commandManager.ExecuteCommandAsync(mainCommand, stackLayout, argument, Typing_Interval);
            
            await InputCommand.TakeCommandAsync(stackLayout, name, Commands); 
        }
    }
}
