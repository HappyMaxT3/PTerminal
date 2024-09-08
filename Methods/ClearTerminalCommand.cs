using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PTerminal.Methods
{
    public class ClearTerminalCommand
    {
        public static async Task ClearTerminal(StackLayout stackLayout, int typingInterval)
        {
            if (stackLayout != null)
            {
                stackLayout.Children.Clear();
            }

            string clearText = "The terminal has been cleaned";

            var label = new Label
            {
                Text = string.Empty,
                TextColor = Colors.White,
                FontFamily = "TerminalFont",
                FontSize = 14,
                LineHeight = 1.2,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };

            stackLayout.Children.Add(label);

            foreach (char c in clearText)
            {
                label.Text += c;
                await Task.Delay(typingInterval);
            }

            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }
    }
}
