namespace PTerminal.Methods
{
    public static class InputCommand
    {
        private static Entry? _currentEntry;

        [Obsolete]
        public static Task TakeCommandAsync(StackLayout stackLayout, string name, Func<string, Task> onCommandEntered)
        {
            if (_currentEntry != null)
            {
                stackLayout.Children.Remove(_currentEntry.Parent as View);
                _currentEntry = null;
            }

            var commandLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 1
            };

            var label = new Microsoft.Maui.Controls.Label
            {
                Text = $"{name} ~ %",
                TextColor = Colors.LightGray,
                FontFamily = "TerminalFont",
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center
            };

            var entryWithPrompt = new Entry
            {
                BackgroundColor = Colors.Transparent,
                TextColor = Colors.White,
                FontFamily = "TerminalFont",
                FontSize = 14,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            entryWithPrompt.Completed += async (sender, e) =>
            {
                var command = entryWithPrompt.Text.Trim();
                entryWithPrompt.Text = string.Empty;

                if (onCommandEntered != null)
                {
                    await onCommandEntered(command);
                }
            };

            _currentEntry = entryWithPrompt;

            commandLayout.Children.Add(label);
            commandLayout.Children.Add(entryWithPrompt);

            stackLayout.Children.Add(commandLayout);

            entryWithPrompt.Focus();
            return Task.CompletedTask;
        }
    }
}
