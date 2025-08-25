using AutoMapper;
using Company_DAL.Data.Models;
using Company_PL.Dtos;

namespace Company_PL.Mapping
{
    public class EmployeeProfile :Profile
    {

        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<Employee, CreateEmployeeDTO>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department));

            //CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
        }
    }
}
