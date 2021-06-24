using System;

namespace MeaMod.Utilities.NetworkTools.PortScan
{
    public class PortScanResultEventArgs : EventArgs
    {
        public PortScanResultEventArgs()
        {
        }

        public PortScanResultEventArgs(string endPoint, int port, PortTypes portType)
          : this()
        {
            this.EndPoint = endPoint;
            this.Port = port;
            this.PortType = portType;
        }

        public int Port { get; set; }

        public string EndPoint { get; set; }

        public PortTypes PortType { get; set; }

        public override string ToString() => string.Format("{0} port {1} found on host {2}.", (object)this.PortType, (object)this.Port, (object)this.EndPoint);
    }
}
