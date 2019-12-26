using IRepository;
using Project.IService;
using Project.Model.Models;
using Project.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service
{
    public class TestService : BaseServices<AdvertisementModel>, ITestService
    {
        public IAdvertisementRepository rpso;
        public TestService(IAdvertisementRepository _rpso)
        {
            this.rpso = _rpso;
            base.baseDal = _rpso;
        }
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
