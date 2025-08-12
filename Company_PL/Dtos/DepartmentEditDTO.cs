using System.ComponentModel.DataAnnotations;

namespace Company_PL.Dtos
{
    public class DepartmentEditDTO
    {
        [Required(ErrorMessage = "code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "date is required")]
        public DateTime CreateAt { get; set; }
    }
}
