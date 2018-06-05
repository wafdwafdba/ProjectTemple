using Dapper;
using IBLL;
using MVC_EF_Temple.BaseCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_EF_Temple.Controllers
{
    public class SqlServerSelectController : Controller
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList(GridPager pager)
        {
            int pagesize = pager.rows;//获取每页显示多少条记录
            int pagenum = pager.page;//获取当前页码
            //int totalRecord;
            var list = new List<AllTable>();
            var list1 = new List<AllTable>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var sql = @"SELECT  o.object_id AS TableID ,
                                        o.name AS TableName,
                                        o.create_date AS CreateDate ,
                                        o.modify_date AS ModifyDate,
                                        CAST(ep.value AS NVARCHAR(MAX)) Comment ,
                                        COUNT(1) AS FieldCount
                                FROM    sys.all_objects o
                                        LEFT JOIN sys.schemas s ON o.schema_id = s.schema_id
                                        LEFT JOIN sys.tables t ON o.object_id = t.object_id
                                        LEFT JOIN sys.extended_properties ep ON ( o.object_id = ep.major_id
                                                                                  AND ep.class = 1
                                                                                  AND ep.minor_id = 0
                                                                                  AND ep.name = 'MS_Description'
                                                                                )
                                        LEFT JOIN ( SELECT  object_id ,
                                                            SUM(rows) row_count
                                                    FROM    sys.partitions
                                                    WHERE   index_id < 2
                                                    GROUP BY object_id
                                                  ) st ON o.object_id = st.object_id
                                        LEFT JOIN sys.change_tracking_tables ct ON o.object_id = ct.object_id
                                        LEFT JOIN sys.all_columns ac ON ac.object_id = o.object_id
                                WHERE   s.name = N'dbo'
                                        AND ( o.type = 'U'
                                              OR o.type = 'S'
                                            )
                                GROUP BY o.object_id ,
                                        o.name ,
                                        o.create_date ,
                                        o.modify_date ,
                                        ep.value
                                ORDER BY o.name";

                list = list1= con.Query<AllTable>(sql).ToList();
            }

            var UIPages = new
            {
                total = list1.Count,
                rows = list.Skip(pagesize * (pagenum - 1))
                             .Take(pagesize).ToList()
            };

            return Json(UIPages, JsonRequestBehavior.AllowGet);
        }
    }

    /// <summary>
    /// 数据库表查询
    /// </summary>
    public class AllTable
    {
        public int TableID { get; set; }
        public string TableName { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public string Comment { get; set; }

        public int FieldCount { get; set; }
    }
}