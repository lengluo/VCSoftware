using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VCSoftware.Util.Log;

namespace VCSoftware.Util
{
    /// <summary>
    /// 提供给外部调用
    /// </summary>
    public static class VCUtil
    {
        //public static ILoggerManager loggerMgr;//单元测试用

        /// <summary>
        /// 全局DIBuilder，用以属性注入获取实例等
        /// </summary>
        public static IApplicationBuilder AppBuilder;

        public static void Register(IApplicationBuilder app, IHostingEnvironment env)
        {
            AppBuilder = app;
            IOC.Context = app.ApplicationServices;
            Path.WebSiteRootPath = env.ContentRootPath;
        }

        #region 日志
        public static class Logger
        {
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
                var loggerMgr = IOC.Resolve<ILoggerManager>();
                //单元测试用静态变量
                var logger = loggerMgr.GetLogger();
                logger.Log(level, msg);
            }
        }

        #endregion

        #region 配置读取

        public static class Config
        {
            private static IConfiguration _configuration;

            public static IConfiguration Configuration
            {
                get
                {
                    if (_configuration == null)
                    {
                        _configuration = new ConfigurationBuilder().SetBasePath(Path.WebSiteRootPath).Add(new JsonConfigurationSource()
                        {
                            Path = "appsettings.json",
                            ReloadOnChange = true
                        }).Build();
                    }
                    return _configuration;
                }
                set { }
            }


            /// <summary>
            /// 初始化配置，同时拷贝至输出目录，单元测试用
            /// </summary>
            /// <param name="path"></param>
            //public static void InitConfig(string basePath, string fileName)
            //{
            //    Configuration = new ConfigurationBuilder().SetBasePath(basePath).Add(new JsonConfigurationSource()
            //    {
            //        Path = fileName,
            //        ReloadOnChange = true
            //    }).Build();
            //}
        }

        #endregion

        #region 路径

        public static class Path
        {
            /// <summary>
            /// 站点根目录
            /// </summary>
            public static string WebSiteRootPath { get; internal set; }
        }

        #endregion

        #region IOC
        public static class IOC
        {
            public static IServiceProvider Context { get; internal set; }

            /// <summary>
            /// 属性注入
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T Resolve<T>() => Context.GetService<T>();
        }

        #endregion
    }
}
