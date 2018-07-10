using Dapper;
using IBLL;
using MVC_EF_Temple.BaseCore;
using MVC_EF_Temple.Models;
using Newtonsoft.Json;
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

        public JsonResult GetList(GridPager pager, string name)
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
                                ";
                if (!string.IsNullOrEmpty(name))
                {
                    sql += $" and (o.name like '%{name}%' or CAST(ep.value AS NVARCHAR(MAX)) like '%{name}%') ";
                }

                sql += @"GROUP BY o.object_id ,
                                        o.name ,
                                        o.create_date ,
                                        o.modify_date ,
                                        ep.value
                                ORDER BY o.create_date desc";
                list = list1 = con.Query<AllTable>(sql).ToList();
            }

            var UIPages = new
            {
                total = list1.Count,
                rows = list.Skip(pagesize * (pagenum - 1))
                             .Take(pagesize).ToList()
            };

            return Json(UIPages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string TableName)
        {
            var strExclude = new string[] { "Timestamp", "SchoolID", "CreateUser", "CreateDate", "UpdateUser", "UpdateDate" };
            List<table_cto> List = new List<table_cto>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string strSQL = $@"  SELECT  '{TableName}' AS TableName,
                                        a1.TableComment,
                                        c.name AS ColumnName ,
                                        c.column_id  AS ColumsID,
                                        t.name AS TypeName ,
                                        c.max_length AS MaxLength ,
                                        c.precision AS Precision,
                                        c.scale AS Scale,
                                        c.collation_name ,
                                        c.is_xml_document ,
                                        CAST(CASE WHEN ( do.parent_object_id = 0 ) THEN 1
                                                  ELSE 0
                                             END AS BIT) AS is_default_binding ,
                                        o3.name rule_name ,
                                        c.is_sparse ,
                                        c.is_column_set ,
                                        c.is_filestream ,
                                        CAST(ep.value AS NVARCHAR(MAX)) AS Comment,
                                        CAST(CASE WHEN c.is_nullable=0 THEN 1 ELSE 0 END AS BIT) AS NotNUll,
                                        1 AS IsCheck,
                                        1 AS IsDataColumn
                                FROM    sys.all_columns c
                                        LEFT JOIN sys.all_objects o ON c.object_id = o.object_id
                                        LEFT JOIN sys.schemas s ON o.schema_id = s.schema_id
                                        LEFT JOIN sys.types t ON c.user_type_id = t.user_type_id
                                        LEFT JOIN sys.all_objects do ON c.default_object_id = do.object_id
                                        LEFT JOIN sys.default_constraints dc ON c.default_object_id = dc.object_id
                                        LEFT JOIN sys.all_objects o3 ON c.rule_object_id = o3.object_id
                                        LEFT JOIN sys.identity_columns id ON c.object_id = id.object_id
                                                                             AND c.column_id = id.column_id
                                        LEFT JOIN sys.computed_columns cc ON c.object_id = cc.object_id
                                                                             AND c.column_id = cc.column_id
                                        LEFT JOIN sys.extended_properties ep ON ( c.object_id = ep.major_id
                                                                                  AND ep.class = 1
                                                                                  AND c.column_id = ep.minor_id
                                                                                  AND ep.name = 'MS_Description'
                                                                                )
                                        OUTER APPLY(
										  SELECT isnull(CONVERT(VARCHAR(max),g.value),'-') AS TableComment,a.name
											FROM  sys.tables a 
											INNER JOIN sys.schemas ss ON ss.schema_id = a.schema_id
											LEFT join sys.extended_properties g  on a.object_id = g.major_id AND g.minor_id = 0
											WHERE a.name=N'{TableName}'
										) a1
                                WHERE   s.name = N'dbo'
                                        AND o.type IN ( 'U', 'S', 'V' )
                                        AND o.name = N'{TableName}'
                                        AND c.name Not IN('{ string.Join("','", strExclude)}')
                                        ORDER BY c.column_id;";
                List = con.Query<table_cto>(strSQL).ToList();
            }
            foreach (var item in List)
            {
                switch (item.TypeName)
                {
                    case "nvarchar":
                        item.MaxLength = (int.Parse(item.MaxLength) / 2).ToString();
                        break;

                    case "decimal":
                        item.MaxLength = $"({item.Precision},{item.Scale})";
                        break;

                    default:
                        item.MaxLength = string.Empty;
                        break;
                }
            }
            ViewBag.list = List;


            return View("~/Views/SqlServerSelect/Edit.cshtml");
        }

        public JsonResult Delete(string tableName)
        {
            PostResult Result = new PostResult();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"drop table [{tableName.Trim()}]";
                var result1 = con.Execute(query);
            }
            Result.Flag = FlagStatus.OK;
            Result.Message ="删除成功！";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public JsonResult Savings(string list, string config)
        {
            List<Table> List= JsonConvert.DeserializeObject<List<Table>>(list);
            ConfigInfo Config= JsonConvert.DeserializeObject<ConfigInfo>(config);

            PostResult Result = new PostResult();
            List<string> strCreateTable = new List<string>();
            List<string> strCreateComment = new List<string>();
            var flag = false;

           // List = List.Where(t => t.IsDataColumn == true).ToList(); //过滤非数据库字段
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var sql=$"SELECT  COUNT(1) FROM dbo.SysObjects WHERE ID = object_id(N'[{Config.TableName.Trim()}]')";
                var count1 = con.Query<int>(sql, new { });
                if (count1.FirstOrDefault() > 0)
                {
                    flag = true;
                    string query = $"drop table [{Config.TableName.Trim()}]";
                    var result1 = con.Execute(query);
                }

                strCreateTable.Add($"CREATE TABLE [dbo].[{Config.TableName.Trim()}](");
                strCreateComment.Add($"EXEC sp_addextendedproperty N'MS_Description', N'{Config.TableComment}', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', NULL, NULL; ");
                foreach (var item in List)
                {
                    string length = item.TypeName.Contains("varchar") ? $"({item.MaxLength.ToString()})" : "";
                    strCreateTable.Add(string.Format($"[{item.ColumnName.Trim()}] {item.TypeName}{{0}} {{1}} {{2}},",
                        length,
                        item.NotNUll ? "Not Null" : "Null",
                        item.DefaultValue?.Length > 0 ? $"'{item.DefaultValue}'" : ""
                        ));
                    strCreateComment.Add($"EXEC sp_addextendedproperty N'MS_Description', N'{item.Comment}', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName.Trim()}', 'COLUMN', N'{item.ColumnName.Trim()}'; ");
                }
              
                strCreateTable.Add(@"[Timestamp] [timestamp] NULL,
                [CreateUser] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
                [CreateDate] [datetime] NULL,
                [UpdateUser] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
                [UpdateDate] [datetime] NULL,");
                if(List[0].NotNUll==true)
                {
                    strCreateTable.Add($"PRIMARY KEY ( [{List[0].ColumnName}] )");
                }
                strCreateTable.Add($");\r\n");
                strCreateComment.Add($@"EXEC sp_addextendedproperty N'MS_Description', N'时间戳', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', 'COLUMN', N'Timestamp' 
                EXEC sp_addextendedproperty N'MS_Description', N'创建人', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', 'COLUMN', N'CreateUser' 
                EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', 'COLUMN', N'CreateDate' 
                EXEC sp_addextendedproperty N'MS_Description', N'修改人', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', 'COLUMN', N'UpdateUser' 
                EXEC sp_addextendedproperty N'MS_Description', N'修改时间', 'SCHEMA', N'dbo', 'TABLE', N'{Config.TableName}', 'COLUMN', N'UpdateDate' ");

                try
                {
                    con.Execute(string.Join("", strCreateTable));
                    con.Execute(string.Join("", strCreateComment));
                }
                catch (Exception)
                {
                    Result.Message = flag == false ? "新增失败！" : "编辑失败！";
                }
              
            }
            Result.Flag = FlagStatus.OK;
            Result.Message = flag==false?"新增成功！":"编辑成功！";
            return Json(Result);
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