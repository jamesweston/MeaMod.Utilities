using System;

namespace MeaMod.Utilities.NetworkTools.TraceRoute
{
    public class TraceRouteNodeFoundEventArgs : EventArgs
    {
        public TraceRouteNodeFoundEventArgs()
        {
        }

        public TraceRouteNodeFoundEventArgs(TraceRouteHopDetail detail)
          : this()
        {
            this.Detail = detail;
        }

        public TraceRouteHopDetail Detail { get; set; }
    }
}
