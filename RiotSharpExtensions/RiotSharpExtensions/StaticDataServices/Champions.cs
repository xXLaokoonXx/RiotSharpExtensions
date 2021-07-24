using RiotSharp.Caching;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.StaticDataServices
{
    public static class Champions
    {
        private static ICache ChampionCache = new Cache();
        private static ChampionListStatic _championList;
        private static ChampionListStatic championList
        {
            get
            {
                var req = new RiotSharp.Http.Requester();
                var StaticData = new RiotSharp.Endpoints.StaticDataEndpoint.DataDragonEndpoints(req, ChampionCache);

                var curversion = StaticData.Versions.GetAllAsync().Result.First();

                if (_championList == null || _championList.Version != curversion)
                {
                    _championList = StaticData.Champions.GetAllAsync(curversion).Result;
                }

                return _championList;
            }
        }

        private const string _riotChampionUrlFormat = "http://ddragon.leagueoflegends.com/cdn/{0}/img/champion/";
        private static string _championIMGUrl
        {
            get { return string.Format(_riotChampionUrlFormat, Versions.CurrentVersion); }
        }

        public static string DefaultChampionIconIMGUrl = "";

        /// <summary>
        /// Function to build the image url for champion icons
        /// </summary>
        /// <param name="id">Champions id</param>
        /// <returns>Build Url for champion icon or <see cref="Champions.DefaultChampionIconIMGUrl"/></returns>
        public static string GetChampionIconIMGUrl(int id)
        {
            try
            {
                var champion = championList.Champions.Where(champ => champ.Value.Id == id).First();

                return _championIMGUrl + champion.Value.Image.Full;
            }
            catch (Exception)
            {
                return DefaultChampionIconIMGUrl;
            }
        }

        /// <summary>
        /// Function to get a champions name based on id
        /// </summary>
        /// <param name="id">Champion id</param>
        /// <returns>Champion name or empty string</returns>
        public static string GetChampionName(int id)
        {
            try
            {
                var champion = championList.Champions.Where(champ => champ.Value.Id == id).First();

                return champion.Value.Name;
            }
            catch (Exception) { return string.Empty; }
        }

        /// <summary>
        /// Function to build the image url for champion icons
        /// </summary>
        /// <param name="id">Champions id</param>
        /// <returns>Champion object from data dragon api</returns>
        public static ChampionStatic GetChampion(int id)
        {
            try
            {
                var champion = championList.Champions.Where(champ => champ.Value.Id == id).First().Value;

                return champion;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
