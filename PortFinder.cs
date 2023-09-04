using System;
using System.IO;
using System.Management;

namespace EppKey
{
    public static class PortFinder
    {
        public static int FindPort0()
        {
            var searcher = new ManagementObjectSearcher("root\\cimv2", @"SELECT * FROM Win32_SerialPort");
            foreach (var queryObj in searcher.Get())
            {
                foreach (var prop in queryObj.Properties)
                {
                    if (prop.Name != "Description" || prop.Value == null ||
                        !Convert.ToString(prop.Value).Contains("Silicon"))
                        continue;

                    var portNum =
                        Convert.ToInt32(Convert.ToString(queryObj.Properties["DeviceID"].Value).Replace("COM", ""));

                    return portNum;
                }
            }

            return -1;
        }

        public static int FindPort1()
        {
            var searcher = new ManagementObjectSearcher("root\\cimv2", @"SELECT * FROM Win32_SerialPort");
            foreach (var queryObj in searcher.Get())
            {
                foreach (var prop in queryObj.Properties)
                {
                    if (prop.Name == "Description" && prop.Value != null && Convert.ToString(prop.Value).Contains("USB Serial Port"))
                    {
                        var portNum =
                            Convert.ToInt32(Convert.ToString(queryObj.Properties["DeviceID"].Value).Replace("COM", ""));
                        return portNum;
                    }
                }
            }
            return -1;
        }

        public static int FindPort2()
        {
            var searcher
                = new ManagementObjectSearcher("root\\cimv2", @"SELECT * FROM Win32_PnPEntity");
            foreach (var queryObj in searcher.Get())
            {
                foreach (var prop in queryObj.Properties)
                {
                    if (prop?.Name == null || prop.Value == null)
                        continue;

                    if (prop.Name == "Name" && prop.Value.ToString().Contains("USB Serial Port ("))
                    {
                        return Convert.ToInt32(prop.Value?.ToString().Replace("USB Serial Port (COM", "").Trim()
                            .Trim(' ', ')', '('));
                    }
                }
            }

            return -1;
        }

        public static int FindPort3()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("root\\cimv2", "SELECT * FROM Win32_PnPEntity"))
                {
                    foreach (var queryObj in searcher.Get())
                    {
                        foreach (var prop in queryObj.Properties)
                        {
                            if (prop?.Name == null || prop.Value == null)
                                continue;

                            if (prop.Name == "Name" && prop.Value.ToString().Contains("USB Serial Port ("))
                            {
                                return Convert.ToInt32(prop.Value?.ToString().Replace("USB Serial Port (COM", "").Trim()
                                    .Trim(' ', ')', '('));
                            }
                        }
                    }

                    Logger.Log("Port Not Found");

                    return -1;
                }
            }
            catch (Exception e)
            {
                Logger.Log($"FindPort3 Error: {e.Message}");

                return -1;
            }
        }

        public static int FindPort4()
        {
            try
            {
                var configs = File.ReadAllLines("Config.ini");
                foreach (var config in configs)
                {
                    if (!config.Contains("BarCod_PORT"))
                        continue;

                    var values = config.Split('=');
                    var port = values[1];
                    if (!port.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var portNum = port.ToUpper().Replace("COM", string.Empty);
                    if (int.TryParse(portNum, out var result))
                    {
                        return result;
                    }
                }

                Logger.Log("Port Not Found");
                return -1;
            }
            catch (Exception e)
            {
                Logger.Log($"FindPort3 Error: {e.Message}");
                return -1;
            }
        }

    }
}
