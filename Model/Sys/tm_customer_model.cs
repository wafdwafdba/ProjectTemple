using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sys
{
    public class tm_customer_model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户ID")]
        public Guid CustomerID { get; set; }
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户名称")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [Display(Name = "联系人名称")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "合同标题")]
        public string ContactTitle { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }
        
        [Display(Name = "城市")]
        public string City { get; set; }

        [Display(Name = "区域")]
        public string Region { get; set; }
        
        [Display(Name = "邮箱")]
        public string PostalCode { get; set; }

        [Display(Name = "国家")]
        public string Country { get; set; }

        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Display(Name = "传真")]
        public string Fax { get; set; }
        
        /// <summary>
        /// pdfurl地址
        /// </summary>
        public string url { get; set; }
    }
}
