﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_BLL.Interfaces;
using Company_DAL.Data.Contexts;
using Company_DAL.Models;

namespace Company_BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository()
        {
            _context = new CompanyDbContext();
        }
        public IEnumerable<Department> GetAll()
        {
            
            return _context.Departments.ToList();
        }
        public Department? Get(int id)
        {
            
            return _context.Departments.Find(id);
        }

        public int Add(Department department)
        {

            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        public int Update(Department department)
        {

            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
        public int Delete(Department department)
        {

            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

      

      
     
    }
}
