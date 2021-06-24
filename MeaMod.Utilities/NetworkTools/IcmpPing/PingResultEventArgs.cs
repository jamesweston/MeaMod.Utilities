using System;
using System.Net.NetworkInformation;

namespace MeaMod.Utilities.NetworkTools.IcmpPing
{
    public class PingResultEventArgs : EventArgs
    {
        public PingResultEventArgs(PingReply reply, Exception exception)
        {
            this.Reply = reply;
            this.Success = reply != null;
            this.LastException = exception;
        }

        public PingReply Reply { get; set; }

        public bool Success { get; set; }

        public Exception LastException { get; set; }

        public override string ToString()
        {
            string str;
            if (this.Success)
                str = string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}", (object)this.Reply.Address, (object)this.Reply.Buffer.Length, (object)this.Reply.RoundtripTime, (object)(this.Reply.Options != null ? this.Reply.Options.Ttl : 0));
            else
                str = this.LastException.InnerException.Message;
            return str;
        }
    }
}
