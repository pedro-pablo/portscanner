using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PortScanner
{
    /// <summary>
    ///     Provides methods to perform port scanning operations.
    /// </summary>
    internal static class PortScan
    {
        /// <summary>
        ///     Tries to establish a TCP connection to the target IP in the target port.
        ///     The property Open of the target port is set to false if the connection fails or true if it succeeds.
        /// </summary>
        /// <param name="targetIp">IP address of the computer which will have a TCP connection established.</param>
        /// <param name="targetPort">TCP port that will be connected to in the target IP.</param>
        /// <param name="timeout">Timeout (in ms).</param>
        public static async Task ScanPortAsync(IPAddress targetIp, PortInfo targetPort, int timeout)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                var result = tcpClient.ConnectAsync(targetIp, targetPort.Port);
                targetPort.Open = await Task.WhenAny(result, Task.Delay(timeout)) == result && result.Exception == null;
            }
        }
    }
}