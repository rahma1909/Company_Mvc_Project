using System.ComponentModel.DataAnnotations;

namespace Company_PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "email is required !!!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required !!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
