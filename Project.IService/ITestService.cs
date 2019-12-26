using Project.IService.Base;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.IService
{
    public interface ITestService : IBaseService<AdvertisementModel>
    {
        int Sum(int a, int b);
    }
}
