using IRepository;
using IRepository.Base;
using Project.Model.Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdvertisementRepository : BaseRepository<AdvertisementModel>, IBaseRespository<AdvertisementModel>
    {
        
    }
}
