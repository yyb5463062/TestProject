using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository.Sugar
{
    public interface IDbContext
    {
        ISqlSugarClient GetClient();
        SimpleClient GetEntityDB();

    }
}
