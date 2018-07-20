using Microsoft.AspNetCore.Mvc;
using System;
using VCSoftware.Auth;
using VCSoftware.Auth.Policy;
using VCSoftware.Dao.Repository;

namespace VCSoftware.Web.Controllers
{
    public class BaseController : Controller
    {
        private ILoginPolicy _loginPolicy = VCSoftware.Util.VCUtil.IOC.Resolve<ILoginPolicy>();

        /// <summary>
        /// 缓存用户信息
        /// </summary>
        protected UserContract CurrentUser
        {
            get
            {
                return _loginPolicy.CurrentUser;
            }
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// <param name="validateRule">验证规则，返回用户ID</param>
        /// <returns></returns>
        public virtual IActionResult SignIn(string userName, string userPassword, Func<string> validateRule)
        {
            //登录验证
            var userId = validateRule.Invoke();
            if (userId == null) throw new Exception("Current user not exists!");
            var userContract = new UserContract
            {
                Id = userId,
                Name = userName
            };
            _loginPolicy.SignIn(userContract);
            return Json(true);
        }
    }
}