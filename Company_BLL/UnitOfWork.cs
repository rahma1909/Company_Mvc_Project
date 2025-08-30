using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_BLL.Interfaces;
using Company_BLL.Repositories;
using Company_DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company_BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public  IDepartmentRepository DepartmentRepository { get; }
       public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
         
        }

        public async Task< int> completeAsync()
        {
            return await _context.SaveChangesAsync();
        }

     
        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}
