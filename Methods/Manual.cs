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
            " NAME       - (ORIGIN) COMMANDS",
            "    man        - (Manual - Linux) Show the entire manual, which explains all the commands and their meaning.",
            "    clear      - (Clear - supported by many OS) Clear terminal completely.",
            "    lshw       - (List Hardware - Linux) Find out the all available info about system.",
            "    ls         - (List - supported by many OS) Shows application directories and files.",
            "    pck        - (Pick - original command) Shows the full path to the picked file on the device."
            
        };

        public static async Task Manuals(StackLayout stackLayout, int TypingInterval)
        {
            var manualText = string.Join(Environment.NewLine, _manual);

            var label = new Label
            {
                Text = string.Empty,
                TextColor = Colors.FloralWhite,
                FontFamily = "TerminalFont",
                FontSize = 14,
                LineHeight = 1.25,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };

            stackLayout.Children.Add(label);

            foreach (char c in manualText)
            {
                label.Text += c;
                await Task.Delay(TypingInterval);
            }

            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }
    }
}
