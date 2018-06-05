using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// DAL通用接口
/// </summary>
namespace IDAL
{
    public partial interface IBaseDAL<T> where T : class, new()
    {
        void Add(T t);

        void Delete(T t);

        void Update(T t);

        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda); //获取实体，根据lamada表达式参数获取

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="isAsc">升序还是降序</param>
        /// <param name="OrderByLambda">根据什么分组</param>
        /// <param name="WhereLambda">条件</param>
        /// <returns></returns>
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, 
            Expression<Func<T, bool>> OrderByLambda, Expression<Func<T, bool>> WhereLambda);

        /// 一个业务中有可能涉及到对多张表的操作,那么可以将操作的数据,打上相应的标记,最后调用该方法,将数据一次性提交到数据库中,避免了多次链接数据库。
        bool SaveChanges();

        IQueryable<T> LoadPageItems<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda, bool isAsc);
    }
}
