using System.ComponentModel.DataAnnotations;

namespace Company_PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "password is required !!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "comfirmpass is required !!!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "comfirm pass doesn't match the pass")]
        public string ComfirmPassword { get; set; }
    }
}
