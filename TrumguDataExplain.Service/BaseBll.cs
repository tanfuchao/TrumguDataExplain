using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrumguDataExplain.Data;

namespace TrumguDataExplain.Service
{
    public class BaseBll<T>:IBaseBll<T> where T:class,new ()
    {

        private readonly IBaseDal<T> _dao = new BaseDal<T>();
        public int Add(T entity)
        {
            return _dao.Add(entity);
        }

        public int AddReturnIdentity(T entity)
        {
            return _dao.AddReturnIdentity(entity);
        }

        public T AddReturnEntity(T entity)
        {
            return _dao.AddReturnEntity(entity);
        }

        public bool AddReturnBool(T entity)
        {
            return _dao.AddReturnBool(entity);
        }

        public int AddBatch(IEnumerable<T> list)
        {
            return _dao.AddBatch(list);
        }

        public int Delete(T entity)
        {
            return _dao.Delete(entity);
        }

        public int Delete(IEnumerable<T> list)
        {
            return _dao.Delete(list);
        }

        public int Delete(int id)
        {
            return _dao.Delete(id);
        }

        public int Delete(int[] ids)
        {
            return _dao.Delete(ids);
        }

        public int Delete(IEnumerable<int> ids)
        {
            return _dao.Delete(ids);
        }

        public int Modify(T entity)
        {
            return _dao.Modify(entity);
        }

        public int ModifyBatch(List<T> list)
        {
            return _dao.ModifyBatch(list);
        }

        public IEnumerable<T> QueryAll()
        {
            return _dao.QueryAll();
        }

        public IEnumerable<T> QueryTop(int top)
        {
            return _dao.QueryTop(top);
        }

        public T QueryById(int id)
        {
            return _dao.QueryById(id);
        }

        public IEnumerable<T> QueryPage(Expression<Func<T, object>> orderBy, int orderType, int pageIndex, int pageSize, ref int totalCount)
        {
            return _dao.QueryPage(orderBy, orderType, pageIndex, pageSize,ref totalCount);
        }

        public IEnumerable<T> QueryByIf(Expression<Func<T, bool>> where)
        {
            return _dao.QueryByIf(where);

        }

        public IEnumerable<T> QueryByIfPage(Expression<Func<T, object>> orderBy, int orderType, Expression<Func<T, bool>> where, int pageIndex, int pageSize, ref int totalCount)
        {
            return _dao.QueryByIfPage(orderBy, orderType,where, pageIndex,pageSize,ref totalCount);
        }
    }
}
