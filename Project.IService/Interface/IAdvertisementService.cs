using Project.IService.Base;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.IService.Interface
{
    public interface IAdvertisementService : IBaseService<AdvertisementModel>
    {
        int Sum(int a, int b);
    }
}
