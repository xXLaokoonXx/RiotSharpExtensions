using RiotSharp.Endpoints.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public static class PlayerSpecificMatchListServices
    {
        /// <summary>
        /// Function to group the <paramref name="matches"/> by champions of the summoner with <paramref name="summonerPuuid"/>
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="summonerPuuid"></param>
        /// <returns>Enumerable with Groupings of an integer indicating the champion 
        /// and the matches containing that champion</returns>
        public static IEnumerable<IGrouping<int, Match>> GetChampionGrouping(this IEnumerable<Match> matches, string summonerPuuid)
        {
            return matches.GetMatchesWithParticipant(summonerPuuid)
                .GroupBy(m => m.GetChampion(summonerPuuid));
        }

        /// <summary>
        /// Function to group the <paramref name="matches"/> by champions of the summoner with <paramref name="summonerPuuid"/>
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="summonerPuuid"></param>
        /// <returns>Enumerable with tuples of championId and count (of the champion)</returns>
        public static IEnumerable<(int championId, int count)> GetChampionList(this IEnumerable<Match> matches, string summonerPuuid)
        { 
            return matches.GetChampionGrouping(summonerPuuid)
                .Select(g => (championId: g.Key, count: g.Count()));
        }

        /// <summary>
        /// Function to reduce the <paramref name="matches"/> received to the matches where the summoner with <paramref name="summonerPuuid"/> takes part in.
        /// </summary>
        /// <param name="matches">Matches to filter through</param>
        /// <param name="summonerPuuid">Summoner to use as a filter</param>
        /// <returns></returns>
        public static IEnumerable<Match> GetMatchesWithParticipant(this IEnumerable<Match> matches, string summonerPuuid)
            => matches.Where(m => m.Info.Participants.Count(p => p.Puuid == summonerPuuid) > 0);
    }
}
