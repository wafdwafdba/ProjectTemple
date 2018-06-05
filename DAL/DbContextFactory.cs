using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// EF上下文对象创建工厂完成
/// </summary>
namespace DAL
{
    //partial仅仅类库部分使用
    public partial class DbContextFactory
    {
        public static DbContext Create() {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
            if(dbContext==null)
            {
                dbContext = new NorthwindEntities();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
