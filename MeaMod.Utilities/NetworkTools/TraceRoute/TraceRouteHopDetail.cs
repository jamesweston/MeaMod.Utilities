using System;

namespace MeaMod.Utilities.NetworkTools.TraceRoute
{
    public class TraceRouteHopDetail
    {
        public TraceRouteHopDetail()
        {
        }

        public TraceRouteHopDetail(
          int hopNumber,
          string ipAddress,
          string hostName,
          TimeSpan responseTime)
          : this()
        {
            this.HopNumber = hopNumber;
            this.IPAddress = ipAddress;
            this.HostName = hostName;
            this.ResponseTime = responseTime;
        }

        public int HopNumber { get; set; }

        public string IPAddress { get; set; }

        public string HostName { get; set; }

        public TimeSpan ResponseTime { get; set; }

        public static string FormattedTextHeader => "Hop IP Address       Response Host" + Environment.NewLine + "--- ---------------- -------- ---------------------------------------" + Environment.NewLine;

        public override string ToString() => string.Format("{0,3} {1,-16} {2,6:N0}ms {3}", (object)this.HopNumber, (object)this.IPAddress, (object)this.ResponseTime.Milliseconds, (object)this.HostName);
    }
}
