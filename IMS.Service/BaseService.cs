using System;
using System.Linq;
using System.Linq.Expressions;
using IMS.IService;
using IMS.IRepository;

namespace IMS.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IBaseRepository<T> BaseRepository { get; set; }

        public BaseService(IBaseRepository<T> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public T Add(T entity) { return BaseRepository.Add(entity); }

        public bool Update(T entity) { return BaseRepository.Update(entity); }

        public bool Delete(T entity) { return BaseRepository.Delete(entity); }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return BaseRepository.Count(predicate);
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return BaseRepository.Exist(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return BaseRepository.Find(whereLambda);
        }
        public IQueryable<T> FindList(IQueryable<T> DataList, Expression<Func<T, bool>> whereLambda)
        {
            return DataList.Select(d => d).Where(whereLambda);
        }
        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        {
            return BaseRepository.FindList(whereLamdba,  orderName,  isAsc);
        }

        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba,  bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            return BaseRepository.FindList(whereLamdba,isAsc, orderLamdba);
        }


        public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        {
            totalRecord = 0;
            return BaseRepository.FindPageList(pageIndex, pageSize,out totalRecord, whereLamdba, orderName, isAsc);
        }

        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba,  bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            totalRecord = 0;
            return BaseRepository.FindPageList(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);
        }
    }
}
