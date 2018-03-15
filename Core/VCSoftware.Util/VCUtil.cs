using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VCSoftware.Util.Log;

namespace VCSoftware.Util
{
    /// <summary>
    /// 提供给外部调用
    /// </summary>
    public class VCUtil
    {
        public static HttpContext UtilContext;
        public class Logger
        {
            public static ILoggerManager loggerMgr;

            /// <summary>
            /// 跟踪
            /// </summary>
            /// <param name="msg"></param>
            public static void Trace(string msg)
            {
                Log(LogLevel.Trace, msg);
            }

            /// <summary>
            /// 公共记录
            /// </summary>
            /// <param name="level"></param>
            private static void Log(LogLevel level, string msg)
            {
                var logger = loggerMgr.GetLogger();
                logger.Log(level, msg);
            }
        }
    }
}
