using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PTerminal
{
    public static class Greetings
    {
        private static readonly List<string> _skeleton = new List<string>
        {
            "░░░░░░░░░░░░░▄▐░░░░",
            "░░░░░░░▄▄▄░░▄██▄░░░ Uhm... ",
            "░░░░░░▐▀█▀▌░░░░▀█▄░",
            "░░░░░░▐█▄█▌░░░░░░▀█   'Hello World'...?",
            "░░░░░░░▀▄▀░░░▄▄▄▄▄▀",
            "░░░░░▄▄▄██▀▀▀▀░░░░░",
            "░░░░█▀▄▄▄█░▀▀░░░░░░",
            "░░░░▌░▄▄▄▐▌▀▀▀░░░░░",
            "░▄░▐░░░▄▄░█░▀▀░░░░░",
            "░▀█▌░░░▄░▀█▀░▀░░░░░",
            "░░░░░░░░▄▄▐▌▄▄░░░░░",
            "░░░░░░░░▀███▀█░▄░░░",
            "░░░░░░░▐▌▀▄▀▄▀▐▄░░░",
            "░░░░░░░▐▀░░░░░░▐▌░░",
            "░░░░░░░█░░░░░░░░█░░",
            "[type 'man' to check the manual]"
        };

        public static async Task TypeGreetingsAsync(StackLayout stackLayout, int typingInterval)
        {
            var skeletonText = string.Join(Environment.NewLine, _skeleton);

            var label = new Label
            {
                Text = string.Empty,
                TextColor = Colors.White,
                FontFamily = "TerminalFont",
                FontSize = 14,
                LineHeight = 1,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };

            stackLayout.Children.Add(label);

            foreach (char c in skeletonText)
            {
                label.Text += c;
                await Task.Delay(typingInterval);
            }

            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }
    }
}
