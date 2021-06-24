using System;
using System.Diagnostics.CodeAnalysis;

namespace MeaMod.Utilities.NetworkTools.PortScan
{
    /// <summary>
    /// Exception class for the MeaMod.Utilities.NetworkTools.PortScan namespace.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PortScanException : NetworkToolsException
    {
        /// <summary>Creates a new instance of this type.</summary>
        public PortScanException()
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        public PortScanException(string message)
          : base(message)
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        /// <param name="inner"></param>
        public PortScanException(string message, Exception inner)
          : base(message, inner)
        {
        }
    }
}
