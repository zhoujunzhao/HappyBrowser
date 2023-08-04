using System;

namespace HappyBrowser.Util
{
    public class UUIDUtil
    {
        public static string NewUUID
        {
            get {
                string uuid = Guid.NewGuid().ToString("N").ToLower();
                uuid = uuid.Replace("-", "");
                return uuid;
            }
        }
    }
}
