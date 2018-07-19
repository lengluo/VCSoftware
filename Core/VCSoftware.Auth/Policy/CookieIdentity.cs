using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace VCSoftware.Auth.Policy
{
    public class CookieIdentity : IIdentity
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CookieIdentity(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 添加Cookie认证声明及规则
        /// </summary>
        /// <param name="user"></param>
        public void SignIn(UserContract user)
        {
            var ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            var cp = new ClaimsPrincipal(ci);
            //写入
            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp);
        }

        /// <summary>
        /// 清除Cookie中的认证声明和规则
        /// </summary>
        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
