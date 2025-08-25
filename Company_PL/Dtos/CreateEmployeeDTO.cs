using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company_DAL.Models;

namespace Company_PL.Dtos
{
    public class CreateEmployeeDTO
    {
        

        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
       [Range(22,60,ErrorMessage ="age must be in range 22,60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="email is not valid")]
        public string Email { get; set; }
        //[RegularExpression("^\\d+\\s+[A-Za-z0-9\\s.,'-]+$\r\n"
        //    ,ErrorMessage = "address must be in format 123 Main Street")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("hiring date")]//name in the frontend
        public DateTime HiringDate { get; set; }
        [DisplayName("date of creation")]
        public DateTime CreateAt { get; set; }
        public int? DepartmentId { get; set; }
        //public Department? Department { get; set; }

        public string? DepartmentName { get; set; }
    }
}
