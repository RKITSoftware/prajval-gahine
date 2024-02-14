using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ServerSideCachingDemo.BL
{
    public class BLCookie
    {
        /// <summary>
        /// next session id that will we assigned to request with no session-id cookie or invalid cookie
        /// </summary>
        public static int nextSessionId;

        public static List<int> lstActiveSession;
        static BLCookie()
        {
            nextSessionId = 1;
            lstActiveSession = new List<int>(10);
        }

        public static bool IsSessionActive(string sessionId)
        {
            return lstActiveSession.Contains(int.Parse(sessionId));
        }
    }
}