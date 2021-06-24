using System;
using System.Collections.Generic;

namespace MeaMod.Utilities.NetworkTools.TraceRoute
{
    public class TraceRouteCompleteEventArgs : EventArgs
    {
        public TraceRouteCompleteEventArgs()
        {
        }

        public TraceRouteCompleteEventArgs(List<TraceRouteHopDetail> traceRouteHops)
          : this()
        {
            this.TraceRouteHops = traceRouteHops;
        }

        public List<TraceRouteHopDetail> TraceRouteHops { get; set; }
    }
}
