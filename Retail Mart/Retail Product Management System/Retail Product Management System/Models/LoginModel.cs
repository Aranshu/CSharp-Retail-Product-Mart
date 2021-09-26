using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For UserLogin
     */
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
