using DAL;
using IDAL;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public abstract partial class BaseService<T> where T : class, new()
    {
        public BaseService()
        {
            SetDal();
        }

        public IBaseDAL<T> Dal { get; set; }

        public abstract void SetDal();

        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChanges();
        }

        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChanges();
        }

        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChanges();
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.GetModels(whereLambda);
        }

        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, bool>> OrderByLambda, Expression<Func<T, bool>> WhereLambda)
        {
            return Dal.GetModelsByPage<type>(pageSize, pageIndex, isAsc, OrderByLambda, WhereLambda);
        }

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
        public IQueryable<T> LoadPageItems<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda, bool isAsc)
        {
            return Dal.LoadPageItems<Tkey>(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        }
    }
}