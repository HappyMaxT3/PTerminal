namespace PTerminal 
{
    public class CommandManager
    {
        private readonly Dictionary<string, Command> _commands = new Dictionary<string, Command>();

        public CommandManager()
        {
            //all comands, ExecuteCommandAsync class instances

            _commands["man"] = new ManCommand();
            _commands["clear"] = new ClearCommand();
            _commands["cd"] = new CdCommand();
            _commands["ls"] = new LsCommand();
            _commands["rm"] = new RmCommand();
            _commands["lshw"] = new SysInfoCommand();
            
        }

        public async Task ExecuteCommandAsync(string commandName, StackLayout stackLayout, string argument, int typingInterval)
        {
            if (_commands.ContainsKey(commandName))
            {
                await _commands[commandName].ExecuteAsync(stackLayout, argument, typingInterval);
            }
            else
            {
                await Methods.ErrorHandler.ShowErrorAsync(stackLayout, $"Command '{commandName}' not found :(", typingInterval);
            }
        }
    }

}
