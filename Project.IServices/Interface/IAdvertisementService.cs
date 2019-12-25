using Project.IServices.Base;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.IServices.Interface
{
    public interface IAdvertisementService : IBaseServices<AdvertisementModel>
    {
        int Sum(int a, int b);
    }
}
