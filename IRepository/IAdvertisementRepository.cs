using IRepository.Base;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IRepository
{
    public interface IAdvertisementRepository:IBaseRespository<AdvertisementModel>
    {
        int Sum(int i, int j);
    }
}
