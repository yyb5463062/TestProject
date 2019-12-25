using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IRepository
{
    public interface IAdvertisementRepository
    {
        int Sum(int i, int j);

        int Add(AdvertisementModel model);
        bool Delete(AdvertisementModel model);
        bool Update(AdvertisementModel model);
        List<AdvertisementModel> Query(Expression<Func<AdvertisementModel, bool>> whereExpression);
    }
}
