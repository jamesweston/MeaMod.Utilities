using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MeaMod.Utilities.NetworkTools.TraceRoute
{
    /// <summary>Class for managing a network trace route.</summary>
    public class TraceRouteManager : ITraceRouteManager, INetworkManager
    {
        private IPAddress lastReplyAddress;

        /// <summary>Creates a new instance of this type.</summary>
        public TraceRouteManager() => this.CurrentCancellationTokenSource = new CancellationTokenSource();

        /// <summary>Event for when a trace route node is found.</summary>
        public event EventHandler<TraceRouteNodeFoundEventArgs> TraceRouteNodeFound;

        /// <summary>Event for when the trace route is complete.</summary>
        public event EventHandler<TraceRouteCompleteEventArgs> TraceRouteComplete;

        /// <summary>Gets the current cancellation token source.</summary>
        public CancellationTokenSource CurrentCancellationTokenSource { get; private set; }

        /// <summary>Executes the trace route.</summary>
        /// <param name="host">The destination host to which you wish to find the route.</param>
        /// <returns>All of the hops between the current computers and the
        /// <paramref name="host" /> computer.</returns>
        /// <exception cref="T:System.ArgumentException"></exception>
        public Task<IEnumerable<TraceRouteHopDetail>> ExecuteTraceRoute(
          string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("Argument \"host\" cannot be null or empty.", nameof(host));
            return Task<IEnumerable<TraceRouteHopDetail>>.Factory.StartNew((Func<IEnumerable<TraceRouteHopDetail>>)(() =>
           {
               List<TraceRouteHopDetail> traceRouteHops = new List<TraceRouteHopDetail>();
               using (Ping ping = new Ping())
               {
                   PingOptions options = new PingOptions(1, true);
                   byte[] buffer = new byte[32];
                   PingReply pingReply = ping.Send(host, 5000, buffer, options);
                   while (!this.CurrentCancellationTokenSource.IsCancellationRequested)
                   {
                       if (pingReply.Address == null)
                       {
                           TraceRouteHopDetail detail = new TraceRouteHopDetail(options.Ttl, "*", "***Request Timed Out***", new TimeSpan());
                           traceRouteHops.Add(detail);
                           this.TraceRouteNodeFound?.Invoke(this, new TraceRouteNodeFoundEventArgs(detail));
                       }
                       else
                       {
                           string hostName = pingReply.Address.ToString();
                           try
                           {
                               hostName = Dns.GetHostEntry(pingReply.Address).HostName;
                           }
                           catch (SocketException)
                           {
                           }
                           TimeSpan responseTime = this.GetResponseTime(pingReply.Address.ToString());
                           if (!pingReply.Address.Equals((object)this.lastReplyAddress) && !pingReply.Address.ToString().Equals(host))
                           {
                               TraceRouteHopDetail detail = new TraceRouteHopDetail(options.Ttl, pingReply.Address.ToString(), hostName, responseTime);
                               traceRouteHops.Add(detail);
                               if (this.TraceRouteNodeFound != null)
                                   this.TraceRouteNodeFound((object)this, new TraceRouteNodeFoundEventArgs(detail));
                           }
                           else
                               break;
                       }
                       if (options.Ttl < 30)
                       {
                           this.lastReplyAddress = pingReply.Address;
                           ++options.Ttl;
                           pingReply = ping.Send(host, 5000, buffer, options);
                       }
                       else
                           break;
                   }
                   this.TraceRouteComplete?.Invoke((object)this, new TraceRouteCompleteEventArgs(traceRouteHops));
                   return (IEnumerable<TraceRouteHopDetail>)traceRouteHops;
               }
           }), this.CurrentCancellationTokenSource.Token);
        }

        /// <summary>
        /// Gets the response time to the specified <paramref name="host" />.
        /// </summary>
        /// <param name="host">The host to ping.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        public TimeSpan GetResponseTime(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("Argument \"ipAddress\" cannot be null or empty.", nameof(host));
            using (Ping ping = new Ping())
            {
                try
                {
                    return new TimeSpan(0, 0, 0, 0, (int)ping.Send(host).RoundtripTime);
                }
                catch (PingException)
                {
                }
            }
            return TimeSpan.MaxValue;
        }

        /// <summary>Cancels the current execution.</summary>
        public void Cancel() => this.CurrentCancellationTokenSource.Cancel();
    }
}
