
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAL;
using Model.Sys;
using Model;

namespace BLL
{
    public partial class CustomerService : BaseService<customer>, IBLL.ICustomerService
    {
        private ICustomerDAL cusdal = DALContainer.Container.Resolve<ICustomerDAL>();
        public override void SetDal()
        {
            Dal = cusdal;
        }
    }
}
