using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PortScanner
{

    /// <summary>
    /// Provides methods to perform port scanning operations.
    /// </summary>
    static class PortScanner
    {

        /// <summary>
        /// Scans the ports of the target IP and checks if they are open, and the results are set in the Open property of each PortInfo scanned.
        /// The method returns when all the scanning tasks have completed.
        /// </summary>
        /// <param name="targetIp">IP address of the computer which will have its ports scanned.</param>
        /// <param name="ports">The ports that will be scanned on the target IP.</param>
        public static async Task Scan(IPAddress targetIp, List<PortInfo> ports)
        {
            List<Task> allTasks = new List<Task>();

            for (int i = 0; i < ports.Count; i++)
            {
                Task scanTask = ScanPortAsync(targetIp, ports[i]);
                allTasks.Add(scanTask);
            }

            await Task.WhenAll(allTasks);
        }


        /// <summary>
        /// Tries to establish a TCP connection to the target IP in the target port.
        /// The property Open of the target port is set to false if the connection fails or true if it succeeds.
        /// </summary>
        /// <param name="targetIp">IP address of the computer which will have a TCP connection established.</param>
        /// <param name="targetPort">TCP port that will be connected to in the target IP.</param>
        private static async Task ScanPortAsync(IPAddress targetIp, PortInfo targetPort)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                await tcpClient.ConnectAsync(targetIp, targetPort.Port);
                targetPort.Open = true;
            }
            catch
            {
                targetPort.Open = false;
            }
            finally
            {
                tcpClient.Close();
            }
        }

    }
}
