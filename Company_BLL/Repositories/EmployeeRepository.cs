using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_BLL.Interfaces;
using Company_DAL.Data.Contexts;
using Company_DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> ,IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context):base(context)
        {
            _context = context;
        }

        public async Task< List<Employee>?> GetByNameAsync(string name)
        {
          return await  _context.Employees.Include(e=>e.Department).Where(e => e.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
