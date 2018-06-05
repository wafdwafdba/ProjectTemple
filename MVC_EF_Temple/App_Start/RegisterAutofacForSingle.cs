using Autofac;
using Autofac.Integration.Mvc;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC_EF_Temple.App_Start
{
    /// <summary>
    /// IOC依赖注入BAL,DAL以及接口层
    /// </summary>
    public class RegisterAutofacForSingle
    {
        public static void RegisterAutofac()
        {
            ContainerBuilder builder = new ContainerBuilder();

            #region 全自动api注入
            //UI项目只用引用service和repository的接口，不用引用实现的dll。

            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll

            var IServices = Assembly.Load("IBLL");
            var Services = Assembly.Load("BLL");
            var IRepository = Assembly.Load("IDAL");
            var Repository = Assembly.Load("DAL");

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(IRepository, Repository)
             .Where(t => t.Name.EndsWith("Dal"))
             .AsImplementedInterfaces();

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IServices, Services)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();

            #endregion        
           
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); 
        }
    }
}