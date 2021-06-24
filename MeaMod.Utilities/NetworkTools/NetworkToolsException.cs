using System;

namespace MeaMod.Utilities.NetworkTools
{
    /// <summary>
    /// Exception class for the MeaMod.Utilities.NetworkTools namespace.
    /// </summary>
    public class NetworkToolsException : Exception
    {
        /// <summary>Creates a new instance of this type.</summary>
        public NetworkToolsException()
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        public NetworkToolsException(string message)
          : base(message)
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        /// <param name="inner"></param>
        public NetworkToolsException(string message, Exception inner)
          : base(message, inner)
        {
        }
    }
}
