using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EF_Temple.BaseCore
{
    //传过去的post结果集
    public class PostResult
    {
        /// <summary>
        /// 结果，OK,NO
        /// </summary>
        public FlagStatus Flag { get; set; }

        /// <summary>
        /// 输出的值
        /// </summary>
        public string Message { get;set;}
    }
}