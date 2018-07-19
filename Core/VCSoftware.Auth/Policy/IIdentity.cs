using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace VCSoftware.Auth.Policy
{
    public interface IIdentity
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userId">用户ID</param>
        void SignIn(UserContract user);

        /// <summary>
        /// 登出
        /// </summary>
        void SignOut();
    }
}
