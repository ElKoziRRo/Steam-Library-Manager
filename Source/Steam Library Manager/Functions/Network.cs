using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Steam_Library_Manager.Functions
{
    internal static class Network
    {
        public static string GetPublicIP()
        {
            try
            {
                return new WebClient().DownloadString("http://icanhazip.com").Replace("\n", "");
            }
            catch { return "127.0.0.1"; }
        }

        public static List<string> GetIpAdresses()
        {
            var iplist = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(x => x.ToString()).ToList();

            iplist.Add(GetPublicIP());

            return iplist;
        }

        public static int GetAvailablePort()
        {
            try
            {
                List<int> usedPorts = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(p => p.Port).ToList();
                int unusedPort = 0;

                for (int port = 50000; port < 64000; port++)
                {
                    if (!usedPorts.Contains(port))
                    {
                        unusedPort = port;
                        break;
                    }
                }

                return unusedPort;
            }
            catch
            {
                return 19000;
            }
        }

        public static bool GetPortStatus(int port)
        {
            try
            {
                List<int> usedPorts = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(p => p.Port).ToList();

                return usedPorts.Contains(port);
            }
            catch
            {
                return true;
            }
        }
    }
}