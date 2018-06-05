using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace MVC_EF_Temple.BaseCore
{
    public class GridPager
    {
        public int rows { get; set; }//每页行数
        public int page { get; set; }//当前页是第几页
        public string order { get; set; }//排序方式
        public string sord { get; set; }//排序列
        public int totalRows { get; set; }//总行数

        public int totalPages //总页数
        {
            get
            {
                return (int)Math.Ceiling((float)totalRows / (float)rows);
            }
        }

    }
}