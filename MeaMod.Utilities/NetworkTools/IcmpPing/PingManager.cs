using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MeaMod.Utilities.NetworkTools.IcmpPing
{
    /// <summary>Class who can perform an ICMP ping to a network host.</summary>
    public class PingManager : IPingManager, INetworkManager
    {
        private Task pingTask;
        private CancellationTokenSource pingCancellationToken = new CancellationTokenSource();

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="hostName">The hostname to ping.</param>
        /// <param name="timeBetweenPings">The time span in between ping attempts.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="T:System.ArgumentException"></exception>
        public PingManager(string hostName, TimeSpan timeBetweenPings)
        {
            if (string.IsNullOrWhiteSpace(hostName))
                throw new ArgumentException("Argument \"HostName\" cannot be null or empty.", nameof(hostName));
            if (timeBetweenPings.TotalSeconds < 1.0)
                throw new ArgumentOutOfRangeException(nameof(timeBetweenPings), "Argument \"timeBetweenPings\" cannot be less than 1 second.");
            this.HostName = hostName;
            this.TimeBetweenPings = timeBetweenPings;
        }

        /// <summary>Gets the current hostname who will be pinged.</summary>
        public string HostName { get; protected set; }

        /// <summary>Gets the time span in between ping attempts.</summary>
        public TimeSpan TimeBetweenPings { get; protected set; }

        /// <summary>
        /// Event for when the state of this ping manager changes. (e.g. starting, stopping, etc)
        /// </summary>
        public event EventHandler<PingManagerStateChangedEventArgs> PingManagerStateChanged;

        /// <summary>
        /// Event for when the ping process determines a result, including no response.
        /// </summary>
        public event EventHandler<PingResultEventArgs> PingResult;

        /// <summary>
        /// Starts the pinging process. Check the <see cref="E:MeaMod.Utilities.NetworkTools.IcmpPing.PingManager.PingResult" /> event for the async results of the ping.
        /// </summary>
        public virtual void Start()
        {
            if (this.pingTask != null && this.pingTask.Status == TaskStatus.Running)
                return;
            this.pingCancellationToken = new CancellationTokenSource();
            this.pingTask = Task.Factory.StartNew(new Action(this.ExecutePing), this.pingCancellationToken.Token);
        }

        /// <summary>Triggers the stop of the pinging process.</summary>
        public virtual void Stop()
        {
            if (this.pingTask != null && this.pingTask.Status == TaskStatus.Running && this.pingCancellationToken != null)
                this.pingCancellationToken.Cancel();
            this.OnStateChangedEvent(PingManagerStates.Stopped);
        }

        /// <summary>Actually execute the ping.</summary>
        protected virtual void ExecutePing()
        {
            while (!this.pingCancellationToken.Token.IsCancellationRequested)
            {
                this.OnStateChangedEvent(PingManagerStates.Pinging);
                using (Ping ping = new Ping())
                {
                    try
                    {
                        this.OnPingResult(ping.Send(this.HostName, 5000), (Exception)null);
                    }
                    catch (PingException ex)
                    {
                        this.OnPingResult((PingReply)null, (Exception)ex);
                    }
                    catch (SocketException ex)
                    {
                        this.OnPingResult((PingReply)null, (Exception)ex);
                    }
                }
                this.OnStateChangedEvent(PingManagerStates.Idle);
                Thread.Sleep(this.TimeBetweenPings);
            }
        }

        /// <summary>
        /// Event handler for when the ping manager state is about to change.
        /// </summary>
        /// <param name="newState">The new state of the ping manager.</param>
        protected virtual void OnStateChangedEvent(PingManagerStates newState)
        {
            if (this.PingManagerStateChanged == null)
                return;
            this.PingManagerStateChanged((object)this, new PingManagerStateChangedEventArgs(newState));
        }

        /// <summary>
        /// Event handler for when the ping manager state is about to change.
        /// </summary>
        /// <param name="reply">The reply from the ping request, or null.</param>
        /// <param name="exception">The exception that was thrown, if any, or null.</param>
        protected virtual void OnPingResult(PingReply reply, Exception exception)
        {
            if (this.PingResult == null)
                return;
            this.PingResult((object)this, new PingResultEventArgs(reply, exception));
        }
    }
}
