using System;

namespace MeaMod.Utilities.NetworkTools.IcmpPing
{
    public class PingManagerStateChangedEventArgs : EventArgs
    {
        public PingManagerStateChangedEventArgs(PingManagerStates newState) => this.NewState = newState;

        public PingManagerStates NewState { get; set; }
    }
}
