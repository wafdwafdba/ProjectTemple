using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface IBaseService<T> where T : class, new()
    {
        bool Add(T t);
        bool Delete(T t);
        bool Update(T t);
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, bool>> OrderByLambda, Expression<Func<T, bool>> WhereLambda);

        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <typeparam name="Tkey">泛型</typeparam>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>IQueryable 泛型集合</returns>
        IQueryable<T> LoadPageItems<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda, bool isAsc);
  
    }
}
