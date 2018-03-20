using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
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

        #region 日志
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

        #endregion

        #region 配置读取

        public class Config
        {

            public static IConfiguration Configuration { get; set; }
            static Config()
            {
                Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource() { Path = "", ReloadOnChange = true }).Build();
            }
        }

        #endregion
    }
}
