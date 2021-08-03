using RiotSharp.Endpoints.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public static class TeamSpecificMatchServices
    {
        public static int? GetTeamId(this Match match, IEnumerable<string> teamSummoners)
        {
            int? teamId = null;
            foreach(var summoner in teamSummoners)
            {
                var p = match.GetParticipant(summoner);
                if(p == null)
                {
                    return null;
                }
                if(teamId != null)
                {
                    if(teamId != p.TeamId)
                    {
                        return null;
                    }
                    teamId = p.TeamId;
                }
            }
            return teamId;
        }

        public static TeamStats GetTeam(this Match match, int teamId)
        {
            return match.Info.Teams.First(t => t.TeamId == teamId);
        }

        public static IEnumerable<TeamBan> GetFirstRotationBans(this TeamStats teamStats)
        {
            return teamStats.Bans.Where(b => b.PickTurn <= 3);
        }

        public static IEnumerable<TeamBan> GetSecondRotationBans(this TeamStats teamStats)
        {
            return teamStats.Bans.Where(b => b.PickTurn > 3);
        }
    }
}
