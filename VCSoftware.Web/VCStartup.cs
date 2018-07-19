﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VCSoftware.Auth.Policy;
using VCSoftware.Util;
using VCSoftware.Util.Log;

namespace VCSoftware.Web
{
    public class VCStartup
    {
        private string _loginPath = "/login";
        protected virtual string LoginPath
        {
            get
            {
                return _loginPath;
            }
            set
            {
                _loginPath = value;
            }
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//MVC
            //日志
            services.AddSingleton<ILoggerManager, Log4NetLoggerManager>();
            services.AddScoped<ILogger, Log4NetLogger>();
            //HTTP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //身份认证及授权
            services.AddSingleton<IIdentity, CookieIdentity>();
            services.AddSingleton<ILoginPolicy, DefaultLoginPolicy>();
            services.AddAuthentication(l =>
            {
                l.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(l =>
            {
                l.LoginPath = _loginPath;
                l.Cookie.Name = "AUTH_USER_COOKIE";
                l.Cookie.Path = "/";
                l.Cookie.HttpOnly = true;
                l.Cookie.Expiration = new System.TimeSpan(365, 0, 0, 0);
                l.ExpireTimeSpan = new System.TimeSpan(365, 0, 0, 0);
            });
            //Session
            services.AddSession(op =>
            {
                op.IdleTimeout = System.TimeSpan.FromMinutes(20);
                op.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //Session，需先注册
            app.UseSession();

            //身份认证及权限
            app.UseAuthentication();
            //注册mvc路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //注册工具库
            VCUtil.Register(app, env);
        }
    }
}
