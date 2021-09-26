using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For Finding Out Vendor For a Product
     */
    public class VendorProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int VendorId { get; set; }
    }
}
