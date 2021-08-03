using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharpExtensions.StaticDataServices
{
    public static class Items
    {
        public static string GetItemIconAdress(long id)
        {
            if (id == 0)
            {
                return "";
            }

            var uri = "http://ddragon.leagueoflegends.com/cdn/{0}/img/item/{1}.png";
            return string.Format(uri, Versions.CurrentVersion, id);
        }
    }
}
