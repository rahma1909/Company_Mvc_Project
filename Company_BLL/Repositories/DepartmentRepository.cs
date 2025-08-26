using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_BLL.Interfaces;
using Company_DAL.Data.Contexts;
using Company_DAL.Models;

namespace Company_BLL.Repositories
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository

    {
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }
    }
}
