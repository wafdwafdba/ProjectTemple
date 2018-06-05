using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EF_Temple.BaseCore
{
    public enum FlagStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 1,
        /// <summary>
        /// 失败
        /// </summary>
        NO = 2,
        /// <summary>
        /// 错误
        /// </summary>
        ERROR = 3,
        /// <summary>
        /// 未登录
        /// </summary>
        UNLOGIN = 4
    }
}