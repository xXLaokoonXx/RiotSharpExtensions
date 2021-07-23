using RiotSharp.Endpoints.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp.Endpoints.MatchEndpoint.Enums;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public class PlayerSpecificTimelineServices
    {
        /// <summary>
        /// Get the champion kills performed between two timepoints by <paramref name="participantId"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static int GetKillsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, int participantId)
        {
            return GetEventsFromTo(timeline, start, end)
                .Count(ev => ev.EventType == MatchEventType.ChampionKill &&
                ev.KillerId == participantId);
        }

        /// <summary>
        /// Get the champion assists performed between two timepoints by <paramref name="participantId"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static int GetAssistsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, int participantId)
        {
            return GetEventsFromTo(timeline, start, end)
                .Count(ev => ev.EventType == MatchEventType.ChampionKill &&
                ev.AssistingParticipantIds.Contains(participantId));
        }

        /// <summary>
        /// Get the deaths performed between two timepoints by <paramref name="participantId"/>
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start timepoint</param>
        /// <param name="end">End timepoint</param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static int GetDeathsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end, int participantId)
        {
            return GetEventsFromTo(timeline, start, end)
                .Count(ev => ev.EventType == MatchEventType.ChampionKill &&
                ev.VictimId == participantId);
        }

        /// <summary>
        /// Private function to recieve all events between two timestemps,
        /// including the timestemps themself
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start of the time</param>
        /// <param name="end">End of the time</param>
        /// <returns></returns>
        private static IEnumerable<MatchEvent> GetEventsFromTo(MatchTimeline timeline, TimeSpan start, TimeSpan end)
        {
            foreach(var frame in timeline.Info.Frames.Where(f => f.Timestamp >= start && f.Timestamp <= end))
            {
                foreach(var ev in frame.Events.Where(e => e.Timestamp >= start && e.Timestamp <= end))
                {
                    yield return ev;
                }
            }
        }

    }
}
