using System;
using System.Linq;
using System.Linq.Expressions;
using IMS.IService;
using IMS.IRepository;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections.Generic;

namespace IMS.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> baseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public async Task<TEntity> QueryByID(object objId)
        {
            return await baseDal.QueryByID(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await baseDal.QueryByID(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await baseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await baseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await baseDal.Update(entity);
        }
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await baseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }



        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await baseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return await baseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await baseDal.QueryPage(whereExpression,
         intPageIndex = 0, intPageSize, strOrderByFileds);
        }

    }
    //public class BaseService<T> : IBaseService<T> where T : class
    //{
    //    protected IBaseRepository<T> BaseRepository { get; set; }


    //    public BaseService(IBaseRepository<T> baseRepository)
    //    {
    //        BaseRepository = baseRepository;
    //    }

    //    public async Task<int> Add(T entity)
    //    {
    //        return await BaseRepository.Add(entity);
    //    }

    //    public async Task<bool> Update(T entity)
    //    {
    //        return await BaseRepository.Update(entity);
    //    }

    //    public async Task<bool> Delete(T entity)
    //    {
    //        return await BaseRepository.Delete(entity);
    //    }

    //    public async Task<int> Count(Expression<Func<T, bool>> predicate)
    //    {
    //        return await BaseRepository.Count(predicate);
    //    }

    //    public bool Exist(Expression<Func<T, bool>> anyLambda)
    //    {
    //        return BaseRepository.Exist(anyLambda);
    //    }
    //    public async Task<T> Find(int id)
    //    {
    //        return await BaseRepository.Find(id);
    //    }
    //    public async Task<T> Find(T entity)
    //    {
    //        return await BaseRepository.Find(entity);
    //    }

    //    public async Task<T> Find(Expression<Func<T, bool>> whereLambda)
    //    {
    //        return await BaseRepository.Find(whereLambda);
    //    }
    //    public async Task<IQueryable<T>> FindList()
    //    {
    //        return await BaseRepository.FindList();
    //    }
    //    public async Task<IQueryable<T>> FindList(Expression<Func<T, bool>> whereLambda)
    //    {
    //        return await BaseRepository.FindList(whereLambda);
    //    }
    //    public IQueryable<T> FindList(IQueryable<T> DataList, Expression<Func<T, bool>> whereLambda)
    //    {
    //        return DataList.Select(d => d).Where(whereLambda);
    //    }
    //    public IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
    //    {
    //        return BaseRepository.FindList(whereLamdba,  orderName,  isAsc);
    //    }

    //    public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba,  bool isAsc, Expression<Func<T, S>> orderLamdba)
    //    {
    //        return BaseRepository.FindList(whereLamdba,isAsc, orderLamdba);
    //    }


    //    public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
    //    {
    //        totalRecord = 0;
    //        return BaseRepository.FindPageList(pageIndex, pageSize,out totalRecord, whereLamdba, orderName, isAsc);
    //    }

    //    public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba,  bool isAsc, Expression<Func<T, S>> orderLamdba)
    //    {
    //        totalRecord = 0;
    //        return BaseRepository.FindPageList(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);
    //    }

    //    public void Copy(T toObj, T fromObj)
    //    {
    //        foreach (PropertyInfo p in toObj.GetType().GetProperties())
    //        {
    //            if (fromObj.GetType().GetProperties().Contains(p))
    //            {
    //                p.SetValue(toObj, fromObj.GetType().GetProperty(p.Name).GetValue(fromObj));
    //            }
    //        }
    //    }


    //}
}
