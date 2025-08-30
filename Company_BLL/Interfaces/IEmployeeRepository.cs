using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_DAL.Data.Models;
using Company_DAL.Models;

namespace Company_BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);

        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);


       Task< List<Employee>? >GetByNameAsync(string name);
    }
}
