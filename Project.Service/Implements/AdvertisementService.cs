using Project.IService.Interface;
using Project.Model.Models;
using Project.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Implements
{
    public class AdvertisementService : BaseServices<AdvertisementModel> ,IAdvertisementService
    {
        IAdvertisementService service;
        public AdvertisementService(IAdvertisementService _service)
        {
            this.service = _service;
        }
        public int Sum(int a, int b)
        {
            return 1;
        }
    }
}
