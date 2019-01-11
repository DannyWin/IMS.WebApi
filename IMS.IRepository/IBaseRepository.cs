using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMS.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> QueryByID(object objId);
        Task<TEntity> QueryByID(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);

        Task<int> Add(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);

        Task<bool> Update(TEntity model);
        Task<bool> Update(TEntity entity, string strWhere);

        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        Task<List<TEntity>> Query();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);

        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);


        Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);
        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="entity">数据实体</param>
        ///// <returns>添加后的数据实体</returns>
        //Task<int> Add(T entity);

        ///// <summary>
        ///// 查询记录数
        ///// </summary>
        ///// <param name="predicate">条件表达式</param>
        ///// <returns>记录数</returns>
        //Task<int> Count(Expression<Func<T, bool>> predicate);

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="entity">数据实体</param>
        ///// <returns>是否成功</returns>
        //Task<bool> Update(T entity);

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="entity">数据实体</param>
        ///// <returns>是否成功</returns>
        //Task<bool> Delete(T entity);

        ///// <summary>
        ///// 是否存在
        ///// </summary>
        ///// <param name="anyLambda">查询表达式</param>
        ///// <returns>布尔值</returns>
        //bool Exist(Expression<Func<T, bool>> anyLambda);

        ///// <summary>
        ///// 根据id查找
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<T> Find(int id);
        //Task<T> Find(T entity);

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        ///// <param name="whereLambda">查询表达式</param>
        ///// <returns>实体</returns>
        //Task<T> Find(Expression<Func<T, bool>> whereLambda);
        //Task<IQueryable<T>> FindList();
        //Task<IQueryable<T>> FindList(Expression<Func<T, bool>> whereLambda);

        ///// <summary>
        ///// 查找数据列表
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="whereLamdba">查询表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLamdba">排序表达式</param>
        ///// <returns></returns>
        ////IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);
        //IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba,string orderName, bool isAsc);
        ///// <summary>
        ///// 查找数据列表
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="whereLamdba">查询表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLamdba">排序表达式</param>
        ///// <returns></returns>
        ////IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);
        //IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);

        ///// <summary>
        ///// 查找分页数据列表
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <param name="totalRecord">总记录数</param>
        ///// <param name="whereLamdba">查询表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLamdba">排序表达式</param>
        ///// <returns></returns>
        ////IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);
        //IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc);

        ///// <summary>
        ///// 查找分页数据列表
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <param name="totalRecord">总记录数</param>
        ///// <param name="whereLamdba">查询表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLamdba">排序表达式</param>
        ///// <returns></returns>
        ////IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);
        //IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);

    }
}
