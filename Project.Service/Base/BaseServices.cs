using IRepository.Base;
using Project.IService.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Base
{
    public class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        public IBaseRespository<TEntity> baseDal;//数据仓储基类不用注入，在子类的构造函数中注入

        //public BaseServices(IBaseRepository<TEntity> ibr)
        //{
        //    baseDal = ibr;
        //}

        public async Task<int> Add(TEntity entity)
        {
            return await baseDal.Add(entity);
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await baseDal.Delete(model);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }

        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<TEntity> QueryByID(object objId)
        {
            return await baseDal.QueryByID(objId);
        }

        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await baseDal.QueryByID(objId, blnUseCache);
        }

        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await baseDal.QueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<bool> Update(TEntity model)
        {
            return await baseDal.Update(model);
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }

        public Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }
    }
}
