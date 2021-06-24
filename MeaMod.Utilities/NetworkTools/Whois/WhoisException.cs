using System;

namespace MeaMod.Utilities.NetworkTools.Whois
{
    /// <summary>
    /// Exception class for the MeaMod.Utilities.NetworkTools.Whois namespace.
    /// </summary>
    public class WhoisException : NetworkToolsException
    {
        /// <summary>Creates a new instance of this type.</summary>
        public WhoisException()
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        public WhoisException(string message)
          : base(message)
        {
        }

        /// <summary>Creates a new instance of this type.</summary>
        /// <param name="message">The message to include with this exception.</param>
        /// <param name="inner"></param>
        public WhoisException(string message, Exception inner)
          : base(message, inner)
        {
        }
    }
}
