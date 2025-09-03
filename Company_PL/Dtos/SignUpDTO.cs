using System.ComponentModel.DataAnnotations;

namespace Company_PL.Dtos
{
    public class SignUpDTO
    {
        [Required(ErrorMessage ="UserName is required !!!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FristName is required !!!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required !!!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "email is required !!!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required !!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "comfirmpass is required !!!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="comfirm pass doesn't match the pass")]
        public string ComfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
