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
            /// 调试
            /// </summary>
            /// <param name="msg"></param>
            public static void Debug(string msg)
            {
                Log(LogLevel.Debug, msg);
            }

            /// <summary>
            /// 一般信息
            /// </summary>
            /// <param name="msg"></param>
            public static void Info(string msg)
            {
                Log(LogLevel.Info, msg);
            }

            /// <summary>
            /// 警告
            /// </summary>
            /// <param name="msg"></param>
            public static void Warn(string msg)
            {
                Log(LogLevel.Warn, msg);
            }

            /// <summary>
            /// 错误
            /// </summary>
            /// <param name="msg"></param>
            public static void Error(string msg)
            {
                Log(LogLevel.Error, msg);
            }

            /// <summary>
            /// 严重错误
            /// </summary>
            /// <param name="msg"></param>
            public static void Fatal(string msg)
            {
                Log(LogLevel.Fatal, msg);
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

            }

            /// <summary>
            /// 初始化配置，同时拷贝至输出目录
            /// </summary>
            /// <param name="path"></param>
            public static void InitConfig(string basePath, string fileName)
            {
                Configuration = new ConfigurationBuilder().SetBasePath(basePath).Add(new JsonConfigurationSource()
                {
                    Path = fileName,
                    ReloadOnChange = true
                }).Build();
            }
        }

        #endregion
    }
}
