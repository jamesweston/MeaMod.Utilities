using System;
using System.Diagnostics.CodeAnalysis;

namespace MeaMod.Utilities.NetworkTools.IcmpPing
{
    /// <summary>
    /// Exception class for the MeaMod.Utilities.NetworkTools.IcmpPing namespace.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class IcmpPingException : NetworkToolsException
    {
        /// <summary>Creates a new instance of this type.</summary>
        public IcmpPingException()
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        public IcmpPingException(string message)
          : base(message)
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        /// <param name="inner"></param>
        public IcmpPingException(string message, Exception inner)
          : base(message, inner)
        {
        }
    }
}
