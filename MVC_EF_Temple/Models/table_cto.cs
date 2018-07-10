using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MVC_EF_Temple.Models
{
    public class table_cto
    {
        #region 标准字段

        public List<Table> EntityList { get; set; }

        /// <summary>
        /// 表id
        /// </summary>
        [Required]
        public string TableID { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段ID
        /// </summary>

        public int ColumsID { get; set; }

        /// <summary>
        /// 数据库 字段类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public string MaxLength { get; set; }

        public int? Precision { get; set; }
        public int? Scale { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool NotNUll { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 是否数据库字段
        /// </summary>
        public bool IsDataColumn { get; set; }

        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }

        /// <summary>
        /// 格式化之后的
        /// 用于页面展示 Title
        /// </summary>
        public string CommentSimple { get; set; }

        public string FieldCount { get; set; }

        public string DefaultValue { get; set; }

        /// <summary>
        /// 父级名称 扩展字段 主表字段的名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool? IsPK { get; set; }

        /// <summary>
        /// Csharp 字段类型
        /// </summary>
        public string CsharpType { get; set; }

        #endregion 标准字段
    }
}