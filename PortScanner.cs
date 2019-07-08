using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PortScanner
{

    class PortScanner
    {

        public async Task Scan(IPAddress targetIp, List<PortInfo> ports)
        {
            List<Task> allTasks = new List<Task>();

            for (int i = 0; i < ports.Count; i++)
            {
                Task scanTask = ScanPortAsync(targetIp, ports[i]);
                allTasks.Add(scanTask);
            }

            await Task.WhenAll(allTasks);
        }

        private async Task ScanPortAsync(IPAddress targetIp, PortInfo targetPort)
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

        public void GetIpAddress(string input, out IPAddress ipAddress)
        {
            IPAddress.TryParse(input, out ipAddress);
        }

    }
}
