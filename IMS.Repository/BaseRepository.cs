using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using IMS.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SqlSugar;

namespace IMS.Repository
{
    public class BaseRepository<TEntity> :IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return entityDB; }
            private set { entityDB = value; }
        }
        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }



        public async Task<TEntity> QueryByID(object objId)
        {
            return await Task.Run(() => db.Queryable<TEntity>().InSingle(objId));
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await Task.Run(() => db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(int[] lstIds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().In(lstIds).ToList());
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            var i = await Task.Run(() => db.Insertable(entity).ExecuteReturnIdentity());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return (int)i;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => db.Updateable(entity).ExecuteCommand());
            return i > 0;
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await Task.Run(() => db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
        }

        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            return await Task.Run(() => db.Ado.ExecuteCommand(strSql, parameters) > 0);
        }

        public async Task<bool> Update(
          TEntity entity,
          List<string> lstColumns = null,
          List<string> lstIgnoreColumns = null,
          string strWhere = ""
            )
        {
            IUpdateable<TEntity> up = await Task.Run(() => db.Updateable(entity));
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = await Task.Run(() => up.Where(strWhere));
            }
            return await Task.Run(() => up.ExecuteCommand()) > 0;
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            var i = await Task.Run(() => db.Deleteable(entity).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }



        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await Task.Run(() => entityDB.GetList());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await Task.Run(() => db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => entityDB.GetList(whereExpression));
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList());
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }


        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Blog.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList());
        }



        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Blog.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Blog.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
        }




        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await Task.Run(() => db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression)
            .ToPageList(intPageIndex, intPageSize));
        }


        ////Data Source = (localdb)\ProjectsV13;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //public static string connection { get; set; } //= @"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMS;Integrated Security = True;";//@"server=localhost;userid=root;pwd=mysql;port=3306;database=IMS;sslmode=none;";////
        //static DbContextOptions<DataContext> dbContextOption = new DbContextOptions<DataContext>();
        //static DbContextOptionsBuilder<DataContext> dbContextOptionBuilder = new DbContextOptionsBuilder<DataContext>(dbContextOption);
        //public DataContext context = new DataContext(dbContextOptionBuilder.UseSqlServer(connection).Options);

        //public async Task<int> Add(T entity)
        //{
        //    return await Task.Run(() =>
        //    {
        //        context.Set<T>().Add(entity);
        //        //context.Entry<T>(entity).State = EntityState.Added;
        //        return context.SaveChanges();
        //    });

        //}

        //public async Task<int> Count(Expression<Func<T, bool>> expression)
        //{
        //    return await Task.Run(() =>context.Set<T>().Count(expression));
        //    //return await context.Set<T>().CountAsync(expression);
        //}

        //public async Task<bool> Update(T entity)
        //{
        //    return await Task.Run(() =>
        //    {
        //        //context.Set<T>().Update(entity);
        //        //context.Set<T>().Attach(entity);
        //        //context.Entry<T>(entity).State = EntityState.Modified;
        //        return context.SaveChanges() > 0;
        //    });

        //}

        //public async Task<bool> Delete(T entity)
        //{
        //    return await Task.Run(() =>
        //    {
        //        context.Set<T>().Remove(entity);
        //        //context.Set<T>().Attach(entity);
        //        //context.Entry<T>(entity).State = EntityState.Deleted;
        //        return context.SaveChanges() > 0;
        //    });
        //}

        //public bool Exist(Expression<Func<T, bool>> anyLambda)
        //{
        //    return context.Set<T>().Any(anyLambda);
        //}

        //public async Task<T> Find(int id)
        //{
        //    return await Task.Run(() => context.Set<T>().Find(id));
        //}
        //public async Task<T> Find(T entity)
        //{
        //    return await Task.Run(() => context.Set<T>().FirstOrDefault(e=>e==entity));
        //}

        //public async Task<T> Find(Expression<Func<T, bool>> whereLambda)
        //{
        //    return await Task.Run(()=>context.Set<T>().FirstOrDefault<T>(whereLambda));         
        //}
        //public async Task<IQueryable<T>> FindList()
        //{
        //    return await Task.Run(() => context.Set<T>().AsQueryable());

        //}
        //public async Task<IQueryable<T>> FindList(Expression<Func<T, bool>> whereLamdba)
        //{
        //    return await Task.Run(() => context.Set<T>().Where(whereLamdba));

        //}

        ///// <summary>
        ///// 排序
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <param name="source">原IQueryable</param>
        ///// <param name="propertyName">排序属性名</param>
        ///// <param name="isAsc">是否正序</param>
        ///// <returns>排序后的IQueryable<T></returns>
        //private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        //{
        //    if (source == null) throw new ArgumentNullException("source", "不能为空");
        //    if (string.IsNullOrEmpty(propertyName)) return source;
        //    var _parameter = Expression.Parameter(source.ElementType);
        //    var _property = Expression.Property(_parameter, propertyName);
        //    if (_property == null) throw new ArgumentNullException("propertyName", "属性不存在");
        //    var _lambda = Expression.Lambda(_property, _parameter);
        //    var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
        //    var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
        //    return source.Provider.CreateQuery<T>(_resultExpression);
        //}

        //public IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba,string orderName, bool isAsc)
        //{
        //    var _list = context.Set<T>().Where(whereLamdba);
        //    _list = OrderBy(_list,orderName,isAsc);
        //    return _list;
        //}
        //public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        //{
        //    var _list = context.Set<T>().Where<T>(whereLamdba);
        //    if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
        //    else _list = _list.OrderByDescending<T, S>(orderLamdba);
        //    return _list;
        //}



        //public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        //{
        //    var _list = context.Set<T>().Where<T>(whereLamdba);
        //    totalRecord = _list.Count();
        //    _list = OrderBy(_list, orderName, isAsc).Skip<T>((pageIndex-1)*pageSize).Take<T>(pageSize);
        //    return _list;
        //}
        //public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        //{
        //    var _list = context.Set<T>().Where<T>(whereLamdba);
        //    totalRecord = _list.Count();
        //    if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    return _list;
        //}

    }
}