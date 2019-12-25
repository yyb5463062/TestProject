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
        public ITestService service;
        public TestService(ITestService _service)
        {
            this.service = _service;
        }
    }
}
