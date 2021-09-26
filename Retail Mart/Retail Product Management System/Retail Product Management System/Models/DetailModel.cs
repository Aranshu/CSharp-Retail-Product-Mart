using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For Storing Detail Of Customer In a Session
     */
    public class DetailModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string  Address { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
