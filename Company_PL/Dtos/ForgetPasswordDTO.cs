using System.ComponentModel.DataAnnotations;

namespace Company_PL.Dtos
{
    public class ForgetPasswordDTO
    {

        [Required(ErrorMessage = "email is required !!!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
