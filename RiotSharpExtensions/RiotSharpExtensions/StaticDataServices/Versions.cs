using RiotSharp.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.StaticDataServices
{
    public static class Versions
    {
        public static string CurrentVersion
        {
            get
            {
                return GetCurrentVersion();
            }
        }

        private static string GetCurrentVersion()
        {
            var req = new RiotSharp.Http.Requester();
            var StaticData = new RiotSharp.Endpoints.StaticDataEndpoint.DataDragonEndpoints(req, new Cache());

            return StaticData.Versions.GetAllAsync().Result.First();
        }
    }
}
