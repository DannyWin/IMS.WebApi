using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IMS.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IMS.Repository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : class
    {

        //Data Source = (localdb)\ProjectsV13;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        public static string connection { get; set; } //= @"Data Source=(localdb)\ProjectsV13;Initial Catalog=WebApi;Integrated Security = True;";//@"server=localhost;userid=root;pwd=mysql;port=3306;database=WebApi;sslmode=none;";////
        static DbContextOptions<DataContext> dbContextOption = new DbContextOptions<DataContext>();
        static DbContextOptionsBuilder<DataContext> dbContextOptionBuilder = new DbContextOptionsBuilder<DataContext>(dbContextOption);
        DataContext context = new DataContext(dbContextOptionBuilder.UseSqlServer(connection).Options);

        public T Add(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Added;
            context.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Count(predicate);
        }

        public bool Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Deleted;
            return context.SaveChanges() > 0;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return context.Set<T>().Any(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = context.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">原IQueryable</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>排序后的IQueryable<T></returns>
        private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null) throw new ArgumentNullException("source", "不能为空");
            if (string.IsNullOrEmpty(propertyName)) return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null) throw new ArgumentNullException("propertyName", "属性不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }

        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba,string orderName, bool isAsc)
        {
            var _list = context.Set<T>().Where(whereLamdba);
            _list = OrderBy(_list,orderName,isAsc);
            return _list;
        }
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = context.Set<T>().Where<T>(whereLamdba);
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
            else _list = _list.OrderByDescending<T, S>(orderLamdba);
            return _list;
        }

        

        public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        {
            var _list = context.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip<T>((pageIndex-1)*pageSize).Take<T>(pageSize);
            return _list;
        }
        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = context.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

    }
}