using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.MatchEndpoint.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public static class TeamSpecificTimelineServices
    {

        /// <summary>
        /// Get the champion kills performed between two timepoints by one of <paramref name="participantIds"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantIds">Participants to count the kills for</param>
        /// <returns></returns>
        public static int GetKillsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, IEnumerable<int> participantIds)
        {
            return timeline.GetEventsFromTo(start, end)
                .Count(ev => ev.EventType == MatchEventType.ChampionKill &&
                participantIds.Contains(ev.KillerId));
        }

        /// <summary>
        /// Get the champion assists performed between two timepoints by one of <paramref name="participantIds"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantIds">Participants to count the assists for</param>
        /// <returns></returns>
        public static int GetAssistsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, IEnumerable<int> participantIds)
        {
            return timeline.GetEventsFromTo(start, end)
                .Select(ev => ev.AssistingParticipantIds
                    .Count(p => participantIds.Contains(p))
                    ).Sum();
        }

        /// <summary>
        /// Get the champion deaths performed between two timepoints by one of <paramref name="participantIds"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantIds">Participants to count the deaths for</param>
        /// <returns></returns>
        public static int GetDeathsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, IEnumerable<int> participantIds)
        {
            return timeline.GetEventsFromTo(start, end)
                .Count(ev => ev.EventType == MatchEventType.ChampionKill &&
                participantIds.Contains(ev.KillerId));
        }

        /// <summary>
        /// Get the champion assists performed between two timepoints by one of <paramref name="participantIds"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantIds">Participants to count the assists for</param>
        /// <returns></returns>
        public static int GetKillsWithAssistFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, IEnumerable<int> participantIds)
        {
            return timeline.GetEventsFromTo(start, end)
                .Count(ev => participantIds.Contains(ev.KillerId) &&
                ev.AssistingParticipantIds.Count(p => participantIds.Contains(p)) > 0);
        }
    }

}
