using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.Exceptions
{
    /// <summary>
    /// Exception triggered when a player is not in a game,
    /// but player specific services got used.
    /// </summary>
    public class RiotSharpExtensionsPlayerNotInGame : RiotSharpExtensionException
    {
        /// <summary>
        /// Puuid of the summoner not found in game
        /// </summary>
        public string Puuid { get; set; }
        public RiotSharpExtensionsPlayerNotInGame() : base() { }
        public RiotSharpExtensionsPlayerNotInGame(string puuid) : base()
        {
            Puuid = puuid;
        }
    }
}
