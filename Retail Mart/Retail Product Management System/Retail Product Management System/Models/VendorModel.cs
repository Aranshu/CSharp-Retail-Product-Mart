using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For VenderDetail
     */
    public class VendorModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DeliveryCharge { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
