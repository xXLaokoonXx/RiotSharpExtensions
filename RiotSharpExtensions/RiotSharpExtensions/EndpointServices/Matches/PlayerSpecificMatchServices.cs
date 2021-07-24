using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharpExtensions.Exceptions;
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

        /// <summary>
        /// Function to get the champion of a a specific summoner in a specific game
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="summonerPuuid">Summoner to get the participant id for</param>
        /// <returns>Champion id</returns>
        public static int GetChampion(this Match match, string summonerPuuid, bool throwException = true)
        {
            try
            {
                return match.GetParticipant(summonerPuuid, true).ChampionId;
            }
            catch (RiotSharpExtensionsPlayerNotInGame ex) 
            {
                if (throwException)
                {
                    throw ex;
                }
                return -1;
            }
        }

        public static int GetTeamSide(this Match match, string summonerPuuid, bool throwException = true)
        {
            return match.GetParticipant(summonerPuuid, throwException).TeamId;
        }

        public static string GetSummonerName(this Match match, string summonerPuuid, bool throwException = true)
        {
            return match.GetParticipant(summonerPuuid, throwException)?.SummonerName;
        }

        /// <summary>
        /// Function to exctract the <see cref="Participant"/> of summoner with <paramref name="summonerPuuid"/> from <paramref name="match"/> <br></br>
        /// Triggers <see cref="RiotSharpExtensionsPlayerNotInGame"/> if <paramref name="throwException"/> is true and summoner is not part of the game
        /// </summary>
        /// <param name="match">Match to get the <see cref="Participant"/> from</param>
        /// <param name="summonerPuuid">Puuid of the summoner</param>
        /// <param name="throwException">Flag to throw exception on not finding the participant (true) or return a default value (false)</param>
        /// <returns>Participant or null</returns>
        /// <exception cref="RiotSharpExtensionsPlayerNotInGame">Triggers if <paramref name="throwException"/> is true and summoner is not part of the game</exception>
        public static Participant GetParticipant(this Match match, string summonerPuuid, bool throwException = false)
        {
            if (match.ContainsParticipant(summonerPuuid, throwException))
            {
                return match.Info.Participants.First(p => p.Puuid == summonerPuuid);
            }
            return null;
        }

        /// <summary>
        /// Function to determine whether the summoner with <paramref name="summonerPuuid"/> took part in <paramref name="match"/>
        /// Triggers <see cref="RiotSharpExtensionsPlayerNotInGame"/> if <paramref name="throwException"/> is true and summoner is not part of the game
        /// </summary>
        /// <param name="match">Match to get the <see cref="Participant"/> from</param>
        /// <param name="summonerPuuid">Puuid of the summoner</param>
        /// <param name="throwException">Flag to throw exception on not finding the participant (true) or return a default value (false)</param>
        /// <returns></returns>
        /// <exception cref="RiotSharpExtensionsPlayerNotInGame"></exception>
        public static bool ContainsParticipant(this Match match, string summonerPuuid, bool throwException = false)
        {
            if(match.Info.Participants.Count(p => p.Puuid == summonerPuuid) > 0)
            {
                return true;
            }
            if (throwException)
            {
                throw new RiotSharpExtensionsPlayerNotInGame(summonerPuuid);
            }
            return false;
        }
    }
}
