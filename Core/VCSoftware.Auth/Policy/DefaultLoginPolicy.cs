using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace VCSoftware.Auth.Policy
{
    public class DefaultLoginPolicy : ILoginPolicy
    {
        private IIdentity _identity;
        private IHttpContextAccessor _httpContextAccessor;
        private string _sessionKey4User = "_SESSION_USER_";

        public DefaultLoginPolicy(IIdentity identity, IHttpContextAccessor httpContextAccessor)
        {
            this._identity = identity;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserContract CurrentUser
        {
            get
            {
                UserContract userInfo = null;
                //从Session中获取
                var sessionInfo = _httpContextAccessor.HttpContext.Session.GetString(_sessionKey4User);
                if (!string.IsNullOrEmpty(sessionInfo))
                {
                    userInfo = JsonConvert.DeserializeObject<UserContract>(sessionInfo);
                }
                return userInfo;
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString(_sessionKey4User, JsonConvert.SerializeObject(value));
            }
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userContractHandler">获取用户实例句柄</param>
        public void SignIn(UserContract user)
        {
            _identity.SignIn(user);
            CurrentUser = user;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void SignOut()
        {
            _identity.SignOut();
        }
    }
}
