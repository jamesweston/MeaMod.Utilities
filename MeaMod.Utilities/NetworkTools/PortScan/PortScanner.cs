using System;
using System.Net.Sockets;

namespace MeaMod.Utilities.NetworkTools.PortScan
{
    /// <summary>Class for scanning a specific port.</summary>
    public class PortScanner
    {
        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="endPointName"></param>
        /// <param name="port"></param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="T:System.ArgumentException"></exception>
        public PortScanner(string endPointName, int port)
        {
            if (string.IsNullOrWhiteSpace(endPointName))
                throw new ArgumentException("Argument \"endPointName\" cannot be null or empty.", nameof(endPointName));
            if (port < 1 || port > (int)ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(port), "Argument \"port\" must be greater than zero and less than 65535.");
            this.EndPointName = endPointName;
            this.Port = port;
        }

        /// <summary>Gets the port being scanned.</summary>
        public int Port { get; private set; }

        /// <summary>Gets the endpoint name (hostname, or IP address).</summary>
        public string EndPointName { get; private set; }

        /// <summary>Events for when the connection attempt is complete.</summary>
        public event EventHandler<PortScanResultEventArgs> PortScanResult;

        /// <summary>Begins the connection attempt via TCP.</summary>
        public void AttemptTcpConnectionToPort()
        {
            try
            {
                TcpClient tcpClient = new TcpClient()
                {
                    ExclusiveAddressUse = false
                };
                tcpClient.Connect(this.EndPointName, this.Port);
                tcpClient.Close();
                if (this.PortScanResult == null)
                    return;
                this.PortScanResult((object)this, new PortScanResultEventArgs(this.EndPointName, this.Port, PortTypes.Tcp));
            }
            catch (SocketException)
            {
            }
        }

        /// <summary>Begins the connection attempt via UDP.</summary>
        public void AttemptUdpConnectionToPort()
        {
            try
            {
                UdpClient udpClient = new UdpClient()
                {
                    ExclusiveAddressUse = false
                };
                udpClient.Connect(this.EndPointName, this.Port);
                udpClient.Close();
                if (this.PortScanResult == null)
                    return;
                this.PortScanResult((object)this, new PortScanResultEventArgs(this.EndPointName, this.Port, PortTypes.Udp));
            }
            catch (SocketException)
            {
            }
        }
    }
}
