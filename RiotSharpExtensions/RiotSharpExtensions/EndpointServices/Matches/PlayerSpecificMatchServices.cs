using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public static class PlayerSpecificMatchServices
    {
        /// <summary>
        /// Function to get the participant allies of a certain summoner. 
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summonerPuuid">Puuid of the summoner whos allies should be returned</param>
        /// <returns></returns>
        public static IEnumerable<Participant> GetAllies(this Match match, string summonerPuuid)
            => match.Info.Participants.Where(p => p.TeamId == match.Info.Participants.First(me => me.Puuid == summonerPuuid).TeamId && p.Puuid != summonerPuuid);

        /// <summary>
        /// Function to get the participant allies of a certain summoner. 
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summoner">Summoner whos allies should be returned</param>
        /// <returns></returns>
        public static IEnumerable<Participant> GetAllies(this Match match, Summoner summoner)
            => match.GetAllies(summoner.Puuid);

        /// <summary>
        /// Function to get the allies of a certain summoner. 
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summoner">Summoner whos allies should be returned</param>
        /// <returns>Summoner names of allies</returns>
        public static IEnumerable<string> GetAllieSummonerNames(this Match match, Summoner summoner)
            => match.GetAllies(summoner.Puuid).Select(ally => ally.SummonerName);

        /// <summary>
        /// Function to get the allies of a certain summoner. 
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summonerPuuid">Puuid of summoner whos allies should be returned</param>
        /// <returns>Summoner names of allies</returns>
        public static IEnumerable<string> GetAllieSummonerNames(this Match match, string summonerPuuid)
            => match.GetAllies(summonerPuuid).Select(ally => ally.SummonerName);

        /// <summary>
        /// Function to get the participant id of a certain summoner.
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summonerPuuid">Summoner to get the participant id for</param>
        /// <returns></returns>
        public static int GetParticipantId(this Match match, string summonerPuuid)
        => match.Info.Participants.First(p => p.Puuid == summonerPuuid).ParticipantId;
    }
}
