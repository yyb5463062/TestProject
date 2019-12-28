using IRepository;
using IRepository.Sugar;
using Project.Model.Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class Sys_DictionaryRepostitory : BaseRepository<Sys_Dictionary>, ISys_DictionaryRepostitory
    {
        public Sys_DictionaryRepostitory(IDbContext dbContext):base(dbContext)
        {

        }
    }
}
