using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Example_Authorization.CustomAuthorization
{
    /// <summary>
    /// 適用於 Web Api 的授權
    /// 原始碼: https://github.com/aspnet/AspNetWebStack/blob/master/src/System.Web.Http/AuthorizeAttribute.cs
    /// </summary>
    public class ApiAuthorization : AuthorizeAttribute
    {
        private string _ErrorMessage = string.Empty;

        /// <summary>
        /// 此授權類別的進入點 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// 驗證是否給予授權 
        /// 1. true: 授權
        /// 2. false: 未授權，OnAuthorization 會呼叫 HandleUnauthorizedRequest 處理授權錯誤回應
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // 取得授權資料
            var authorization = actionContext.Request.Headers.Authorization;
            var filterErrorMessage = "Filter 驗證失敗: ";

            // 驗證資料格式
            if (authorization == null)
            {
                // 自訂錯誤回應: 授權不存在
                _ErrorMessage = filterErrorMessage + "Request.Headers.Authorization Not Exist";
            }
            else if (authorization.Scheme != "Bearer")
            {
                // 自訂錯誤回應: 授權格式不符要求
                _ErrorMessage = filterErrorMessage + "Request.Headers.Authorization.Scheme Invalid";
            }
            else if (string.IsNullOrEmpty(authorization.Parameter) || authorization.Parameter == "undefined")
            {
                // 自訂錯誤回應: 授權遺失
                _ErrorMessage = filterErrorMessage + "Request.Headers.Authorization.Parameter Not Exist";
            }
            else
            {
                // 驗證 Token 處理

                // 驗證成功，給予授權
                return true;
            }

            return false;

            // 不使用預設的授權判斷
            // return base.IsAuthorized(actionContext);
        }

        /// <summary>
        /// 處理未授權的回應
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            /* ResponseMessage 要求物件
             * 1. Request: 原始的 Request
             * 2. Content: 回傳的結果，Json.Xml
             */

            // 建立回應訊息，自動加入原始Request
            var response = actionContext.Request.CreateResponse(HttpStatusCode.OK);

            // 建立回應訊息，手動加入原始Request
            // var responseMessage = new HttpResponseMessage(HttpStatusCode.Forbidden);
            // responseMessage.RequestMessage = actionContext.Request;

            // 建立錯誤結果
            var ErrorResult = new Models.ExecutionResultInfoModel()
            {
                IsSuccess = false,
                Message = _ErrorMessage
            };

            // 建立回應 (String Content)
            //response.Content = new StringContent(JsonConvert.SerializeObject(ErrorResult));
            //actionContext.Response = response;

            // 建立回應 (Object Content)
            response.Content = new ObjectContent(ErrorResult.GetType(), ErrorResult, new JsonMediaTypeFormatter());
            actionContext.Response = response;

            // 避免建立的 Response 被預設的 Response 洗掉
            // base.HandleUnauthorizedRequest(actionContext);
        }
    }
}