using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PTerminal.Methods
{
    public static class Manual
    {
        private static readonly List<string> _manual = new List<string>
        {
            "   WARNING:    Some commands (cd, ls, rm) require permissions. For the app work correctly, go to settings and give the necessary permissions to PTerminal.",
            "   NAME       - (ORIGIN) COMMANDS",
            "    man        - (Manual - Linux) Show the entire manual, which explains all the commands and their meaning.",
            "    clear      - (Clear - supported by many OS) Clear terminal completely.",
            "    lshw       - (List Hardware - Linux) Find out the all available info about system.",
            "    cd         - (Change Directory - supported by many OS) Change user's work directory. Android users should select a folder in device Explorer after typing 'cd'.",
            "    ls         - (List - supported by many OS) Shows directories and files in user's work directory.",
            "    rm 'smth'  - (Remove - supported by many OS) Delete selected file."
            
        };

        public static async Task Manuals(StackLayout stackLayout, int typingInterval)
        {
            foreach (string line in _manual)
            {
                var formattedString = new FormattedString();
                var firstPart = line.Length > 14 ? line.Substring(0, 14) : line;
                var secondPart = line.Length > 14 ? line.Substring(14) : string.Empty;

                var label = new Label
                {
                    FormattedText = formattedString,
                    FontFamily = "TerminalFont",
                    FontSize = 14,
                    LineHeight = 1.25,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };

                stackLayout.Children.Add(label);

                formattedString.Spans.Add(new Span
                {
                    Text = string.Empty,  
                    TextColor = Colors.Yellow,
                    FontFamily = "TerminalFont",
                    FontSize = 14
                });

                formattedString.Spans.Add(new Span
                {
                    Text = string.Empty, 
                    TextColor = Colors.FloralWhite,
                    FontFamily = "TerminalFont",
                    FontSize = 14
                });

                for (int i = 0; i < firstPart.Length; i++)
                {
                    formattedString.Spans[0].Text += firstPart[i];
                    await Task.Delay(typingInterval);
                }

                for (int i = 0; i < secondPart.Length; i++)
                {
                    formattedString.Spans[1].Text += secondPart[i];
                    await Task.Delay(typingInterval);
                }

                stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
            }
        }
    }
}
