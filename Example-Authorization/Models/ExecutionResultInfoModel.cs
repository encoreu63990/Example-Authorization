using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example_Authorization.Models
{
    public class ExecutionResultInfoModel
    {
        #region [Initial]
        public ExecutionResultInfoModel()
        {
            IsSuccess = false;
            Message = string.Empty;
            ReturnObject = null;
        }

        #endregion


        #region [Property]
        /// <summary>
        /// Function 是否執行成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 執行結果訊息(正常訊息、錯誤訊息)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 回傳物件(執行後產生的Model、物件...等)
        /// </summary>
        public object ReturnObject { get; set; }

        #endregion
    }
}