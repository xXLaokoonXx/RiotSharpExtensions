using RiotSharp.Endpoints.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.EndpointServices.Matches
{
    public static class BaseTimelineServices
    {
        /// <summary>
        /// Function to recieve all events between two timestemps,
        /// including the timestemps themself
        /// </summary>
        /// <param name="timeline">Timeline to filter events from</param>
        /// <param name="start">Start of the time</param>
        /// <param name="end">End of the time</param>
        /// <returns></returns>
        public static IEnumerable<MatchEvent> GetEventsFromTo(this MatchTimeline timeline, TimeSpan start, TimeSpan end)
        {
            foreach (var frame in timeline.Info.Frames.Where(f => f.Timestamp >= start && f.Timestamp <= end))
            {
                foreach (var ev in frame.Events.Where(e => e.Timestamp >= start && e.Timestamp <= end))
                {
                    yield return ev;
                }
            }
        }
    }
}
