using AutoMapper;
using Company_DAL.Data.Models;
using Company_PL.Dtos;

namespace Company_PL.Mapping
{
    public class EmployeeProfile :Profile
    {

        public EmployeeProfile()
        {
            //CreateMap<CreateEmployeeDTO, Employee>();
            //CreateMap<Employee, CreateEmployeeDTO>();

            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
        }
    }
}
