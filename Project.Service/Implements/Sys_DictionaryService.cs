using IRepository;
using Project.IService.Interface;
using Project.Model.Models;
using Project.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Implements
{
    public class Sys_DictionaryService:BaseServices<Sys_Dictionary>, ISys_DictionaryService
    {
        ISys_DictionaryRepostitory _dal;
        public Sys_DictionaryService(ISys_DictionaryRepostitory dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
