using System.Web;
using System.Web.Mvc;

namespace Example_Authorization.CustomAuthorization
{
    /// <summary>
    /// 適用於 Web Mvc 的授權
    /// </summary>
    public class MvcAuthorization: AuthorizeAttribute
    {
        /// <summary>
        /// 此授權類別的進入點
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 驗證是否給予授權 
        /// 1. true: 授權
        /// 2. false: 未授權，OnAuthorization 呼叫 HandleUnauthorizedRequest 處理授權錯誤回應
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // 驗證 Token 處理

            // 驗證成功，給予授權
            return true;

            /// 不使用預設的授權判斷
            // return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// 處理未授權的回應
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // 預設未授權處理，可由 Web.config 設定自動導向登入頁面
            /*
                <system.web>
                  <authentication mode="Forms"><forms loginUrl="/Home/Login" timeout="2880" /></authentication>
                </system.web>
             */

            // 也可以自定回傳的 ActionResult
            // filterContext.Result = RedirectToAction("view", "controller");

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}