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
       Task< IEnumerable<T>> GetAllAsync();
        Task< T? > GetAsync(int id);

        Task AddAsync(T department);
        void Update(T department);
        void Delete(T department);

    }
}
