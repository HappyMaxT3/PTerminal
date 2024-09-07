namespace PTerminal.Methods
{
    public static class SystemInfo
    {
        private static string GetSystemInfo()
        {
            // var processInfoService = DependencyService.Get<IProcessInfoService>();
            // var processInfoList = processInfoService?.GetAllProcesses() ?? new List<string>();
            // var processInfo = string.Join(Environment.NewLine, processInfoList);

            var systemInfo = new List<string>
            {
                "   SYSTEM INFO:",
                $"         MANUFACTURER: {DeviceInfo.Manufacturer ?? "N/A"}",
                $"         MODEL: {DeviceInfo.Model ?? "N/A"}",
                $"█▀▀▀▀▀█  DEVICE NAME: {DeviceInfo.Name ?? "N/A"}",
                $"█░░░░░█   OS PLATFORM: {DeviceInfo.Platform}",
                $"█░░░░░█   OS VERSION: {DeviceInfo.VersionString ?? "N/A"}",
                $"█░░░░░█  DEVICE TYPE: {DeviceInfo.Idiom}",
                $"█▀█▀█▀█   BATTERY STATE: {GetBatteryState()}",
                $"█▀█▀█▀█   BATTERY LEVEL: {GetBatteryLevel()}",
                $"▀▀▀▀▀▀▀  SCREEN SIZE: { GetScreenSize()}",
                $"         REFRESH RATE: {GetRefreshRate()}",
                $"         DPI: {GetDeviceDensity()}",
                //$"   CPU USAGE: {GetCpuUsage()}%",
                //$"   PROCESS INFO:\n{processInfo}"
            };

            return string.Join(Environment.NewLine, systemInfo);
        }

        // private static string GetCpuUsage()
        // {
        //     try
        //     {
        //         var cpuUsageService = DependencyService.Get<ICpuUsageService>();
        //         float cpuUsage = cpuUsageService?.GetCpuUsage() ?? 0;
        //         return $"{cpuUsage:0.00}";
        //     }
        //     catch
        //     {
        //         return "N/A";
        //     }
        // }

        private static string GetBatteryState()
        {
            try
            {
                return Battery.State.ToString();
            }
            catch
            {
                return "N/A";
            }
        }

        private static string GetBatteryLevel()
        {
            try
            {
                return Battery.ChargeLevel >= 0 ? $"{(Battery.ChargeLevel * 100):0.00}%" : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private static string GetDeviceDensity()
        {
            try
            {
                return DeviceDisplay.MainDisplayInfo.Density >= 0 ? $"{DeviceDisplay.MainDisplayInfo.Density:0.00}" : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private static string GetRefreshRate()
        {
            try
            {
                return DeviceDisplay.MainDisplayInfo.RefreshRate >= 0 ? $"{DeviceDisplay.MainDisplayInfo.RefreshRate:0.00}" : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private static string GetScreenSize()
        {
            try
            {
                var width = DeviceDisplay.MainDisplayInfo.Width >= 0 ? $"{DeviceDisplay.MainDisplayInfo.Width}" : "N/A";
                var height = DeviceDisplay.MainDisplayInfo.Height >= 0 ? $"{DeviceDisplay.MainDisplayInfo.Height}" : "N/A";
                return $"{width}x{height}";
            }
            catch
            {
                return "N/A";
            }
        }

        public static async Task SystemInfoOut(StackLayout stackLayout, int typingInterval)
        {
            var systemInfoText = await Task.Run(() => GetSystemInfo());

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

            foreach (char c in systemInfoText)
            {
                label.Text += c;
                await Task.Delay(typingInterval);
            }

            stackLayout.Children.Add(new Label { Text = "", FontSize = 8 });
        }
    }
}
