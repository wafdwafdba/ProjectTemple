using IBLL;
using MVC_EF_Temple.BaseCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_EF_Temple.Controllers
{
    public class CutomerController : Controller
    {
        private ICustomerService cs = BLLContainer.Container.Resolve<ICustomerService>();
        public ActionResult Index()
        {
            string pdfPrefixUrl = ConfigurationManager.AppSettings["pdfPrefixUrl"].ToString(); //Pdf url前缀地址
            ViewData["pdfPrefixUrl"] = pdfPrefixUrl;

            return View();
        }

        public JsonResult GetList(GridPager pager)
        {
            int pagesize = pager.rows;//获取每页显示多少条记录
            int pagenum = pager.page;//获取当前页码
            int totalRecord;
            //如果ViewData["pdfPrefixUrl"]不对需要配置webconfig的pdfPrefixUrl"的值
           
            var list = cs.LoadPageItems(pagesize, pagenum, out totalRecord, u => u.CustomerID != null, u => u.CustomerID, true).ToList();

            var UIPages = new { total = totalRecord, rows = list };

            return Json(UIPages, JsonRequestBehavior.AllowGet);
        }
       
    }
}