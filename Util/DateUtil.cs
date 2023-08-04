using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBrowser.Util
{
    public class DateUtil
    {
   
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static long GetTimeStamp(bool isMill=false)
        {
            return isMill ? DateTimeOffset.Now.ToUnixTimeSeconds() : DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}
