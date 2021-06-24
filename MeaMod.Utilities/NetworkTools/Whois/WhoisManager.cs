using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MeaMod.Utilities.NetworkTools.Whois
{
    /// <summary>Class for executing Whois requests.</summary>
    public class WhoisManager : IWhoisManager, INetworkManager
    {
        private const string DefaultWhoisLookupFormat = "{0}.whois-servers.net";

        /// <summary>Executes whois for a domain.</summary>
        /// <param name="domain">The domain to lookup.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A text string with the whois information.</returns>
        public string ExecuteWhoisForDomain(string domain)
        {
            string[] strArray = !string.IsNullOrWhiteSpace(domain) ? domain.Split('.') : throw new ArgumentException("Argument \"domain\" cannot be null or empty.", nameof(domain));
            string whoisServer = string.Format("{0}.whois-servers.net", (object)strArray[strArray.Length - 1]);
            return this.ExecuteWhoisForDomain(domain, whoisServer);
        }

        /// <summary>Executes whois for a domain.</summary>
        /// <param name="domain">The domain to lookup.</param>
        /// <param name="whoisServer">The whois server to use.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A text string with the whois information.</returns>
        public string ExecuteWhoisForDomain(string domain, string whoisServer)
        {
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("Argument \"domain\" cannot be null or empty.", nameof(domain));
            IPEndPoint ipEndPoint = !string.IsNullOrWhiteSpace(whoisServer) ? new IPEndPoint(Dns.GetHostEntry(whoisServer).AddressList[0], 43) : throw new ArgumentException("Argument \"whoisServer\" cannot be null or empty.", nameof(whoisServer));
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect((EndPoint)ipEndPoint);
                    byte[] bytes = Encoding.ASCII.GetBytes(string.Format("{0}\r\n", (object)domain));
                    socket.Send(bytes);
                    byte[] numArray = new byte[1024];
                    for (int count = socket.Receive(numArray); count > 0; count = socket.Receive(numArray))
                        stringBuilder.Append(Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", Environment.NewLine));
                    socket.Shutdown(SocketShutdown.Both);
                }
            }
            catch (SocketException)
            {
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Attempts to find the authority whois server, in another whois server output.
        /// </summary>
        /// <param name="whoisOutput">The whois output to search.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A list of all of the found whois servers, or null.</returns>
        public IEnumerable<string> FindWhoisServerInOutput(string whoisOutput)
        {
            if (string.IsNullOrWhiteSpace(whoisOutput))
                throw new ArgumentException("Argument \"whoisOutput\" cannot be null or empty.", nameof(whoisOutput));
            List<string> stringList = new List<string>();
            if (whoisOutput.Contains("Whois Server: "))
            {
                int startIndex1 = 0;
                while (true)
                {
                    int startIndex2 = whoisOutput.IndexOf("Whois Server:", startIndex1);
                    if (startIndex2 > 0)
                    {
                        int num = whoisOutput.IndexOf('\n', startIndex2 + 1);
                        string[] strArray = whoisOutput.Substring(startIndex2, num - startIndex2).Split(':');
                        if (strArray.GetUpperBound(0) > 0)
                            stringList.Add(strArray[1].Trim());
                        startIndex1 = num + 1;
                    }
                    else
                        break;
                }
            }
            return (IEnumerable<string>)stringList;
        }
    }
}
