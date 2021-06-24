﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeaMod.Utilities.NetworkTools.PortScan
{
    /// <summary>Class for managing a port scan.</summary>
    public class PortScanManager : IPortScanManager, INetworkManager
    {
        private readonly List<Task> _tasks = new List<Task>();

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="endPointToScan">The hostname or IP address to scan.</param>
        public PortScanManager(string endPointToScan)
        {
            this.EndPoint = !string.IsNullOrWhiteSpace(endPointToScan) ? endPointToScan : throw new ArgumentException("Argument \"endPointToScan\" cannot be null or empty.", nameof(endPointToScan));
            this.CurrentCancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>Gets the current endpoint to be scanned.</summary>
        public string EndPoint { get; private set; }

        /// <summary>Gets the current cancellation token source.</summary>
        public CancellationTokenSource CurrentCancellationTokenSource { get; private set; }

        /// <summary>Event for when there is a result from the port scan.</summary>
        public event EventHandler<PortScanResultEventArgs> PortScanResult;

        /// <summary>Starts the port scan. Scans ports 1 through 65535.</summary>
        public void Start() => this.Start(1, (int)ushort.MaxValue, PortTypes.Tcp);

        /// <summary>Starts the port scan.</summary>
        /// <param name="startingPortNumber">The port number to inclusively start scanning. (e.g. 1)</param>
        /// <param name="endingPortNumber">The port number to inclusively stop scanning. (e.g. 65535)</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        public void Start(int startingPortNumber, int endingPortNumber, PortTypes typesToScan)
        {
            if (startingPortNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(startingPortNumber), "Argument \"startingPortNumber\" must be greater than zero.");
            if (endingPortNumber > (int)ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(endingPortNumber), "Argument \"endingPortNumber\" must be less than 65535.");
            for (int port = startingPortNumber; port <= endingPortNumber; ++port)
            {
                if (this.CurrentCancellationTokenSource.IsCancellationRequested)
                    break;
                PortScanner scanner = new PortScanner(this.EndPoint, port);
                scanner.PortScanResult += new EventHandler<PortScanResultEventArgs>(this.scanner_PortScanResult);
                switch (typesToScan)
                {
                    case PortTypes.Udp:
                        this._tasks.Add(Task.Factory.StartNew((Action)(() => scanner.AttemptUdpConnectionToPort()), this.CurrentCancellationTokenSource.Token));
                        break;
                    case PortTypes.Tcp:
                        this._tasks.Add(Task.Factory.StartNew((Action)(() => scanner.AttemptTcpConnectionToPort()), this.CurrentCancellationTokenSource.Token));
                        break;
                }
            }
        }

        /// <summary>Stops the port scan.</summary>
        public void Stop() => this.CurrentCancellationTokenSource.Cancel();

        /// <summary>Gets or sets the currently running tasks.</summary>
        public IEnumerable<Task> Tasks => (IEnumerable<Task>)this._tasks;

        private void scanner_PortScanResult(object sender, PortScanResultEventArgs e)
        {
            if (this.PortScanResult == null)
                return;
            this.PortScanResult((object)this, e);
        }
    }
}
