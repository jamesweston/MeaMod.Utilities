using System.Collections.Generic;

namespace MeaMod.Utilities.NetworkTools.Whois
{
    /// <summary>Interface for classes who execute "whois" requests.</summary>
    public interface IWhoisManager : INetworkManager
    {
        /// <summary>Executes whois for a domain.</summary>
        /// <param name="domain">The domain to lookup.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A text string with the whois information.</returns>
        string ExecuteWhoisForDomain(string domain);

        /// <summary>Executes whois for a domain.</summary>
        /// <param name="domain">The domain to lookup.</param>
        /// <param name="whoisServer">The whois server to use.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A text string with the whois information.</returns>
        string ExecuteWhoisForDomain(string domain, string whoisServer);

        /// <summary>
        /// Attempts to find the authority whois server, in another whois server output.
        /// </summary>
        /// <param name="whoisOutput">The whois output to search.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <returns>A list of all of the found whois servers, or null.</returns>
        IEnumerable<string> FindWhoisServerInOutput(string whoisOutput);
    }
}
