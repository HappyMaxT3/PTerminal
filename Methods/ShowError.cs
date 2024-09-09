namespace PTerminal.Methods
{
    public static class ErrorHandler
    {
        public static async Task ShowErrorAsync(StackLayout stackLayout, string errorMessage, int typingInterval)
        {
            var label = new Label
            {
                Text = string.Empty,
                TextColor = Colors.Red,
                FontFamily = "TerminalFont",
                FontSize = 14,
                LineHeight = 1
            };

            stackLayout.Children.Add(label);

            foreach (char c in errorMessage)
            {
                label.Text += c;
                await Task.Delay(typingInterval);
            }
        }
    }
}
