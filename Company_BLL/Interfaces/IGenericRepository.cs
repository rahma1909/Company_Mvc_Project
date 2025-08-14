﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_DAL.Data.Models;

namespace Company_BLL.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);

        int Add(T department);
        int Update(T department);
        int Delete(T department);

    }
}
